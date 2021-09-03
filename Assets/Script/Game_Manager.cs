using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Player;

[System.Serializable]
public class StageList
{
    public int[] Chapter;
}

public class Game_Manager : MonoBehaviourPun
{
    //Stage[1].Chapter[0]
    public StageList[] Stage;
    public StageList[] Stage2;

    public int CurrentChapter = 1;
    public int CurrentStage = 1;
    public bool CanNextStage = false;

    public int gameClear = 0;

    public static Game_Manager instance;
    public GameObject playerPrefab;
    public static GameObject LocalPlayerInstance;
    SpriteRenderer spriteRenderer;

    public int life = 5;
    public Vector3 tmp;

    public int minSize;
    private void Update()
    {
        if(Stage[CurrentStage-1].Chapter[CurrentChapter-1] == 0)
        {
            CanNextStage = true;
        }
    }

    public void Clear()
    {
        GameObject thisStage = GameObject.Find(CurrentStage + "-" + CurrentChapter);
        Reset[] mItems = thisStage.GetComponentsInChildren<Reset>() as Reset[];
        foreach (Reset r in mItems)
        {
            // 맵 오브젝트 위치 및 값 초기화
            r.PositionReset();
        }
        gameClear = 0;
        CanNextStage = false;
        Stage[CurrentStage - 1].Chapter[CurrentChapter - 1]
            = Stage2[CurrentStage - 1].Chapter[CurrentChapter - 1];
    }
    public void LoadLobby()
    {
        PhotonNetwork.LeaveRoom();
        Invoke("OnLeftRoom", 0.3f);

    }
    public void OnLeftRoom()
    {
        Destroy(GameObject.Find("Canvas")); 
        var obj = FindObjectsOfType<Game_Manager>();
        Destroy(obj[0].gameObject);
        SceneManager.LoadScene("Lobby");
    }

    [PunRPC]
    public void rpcPass()
    {
        ++gameClear;
    }
    [PunRPC]
    public void NextChapter()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == gameClear)
        {
            CanNextStage = false;
            CurrentChapter += 1;
            if (CurrentChapter == minSize+1)
            {
                CurrentChapter = 1;
                CurrentStage += 1;
                GameStart();
            }

            ViewChanger.instance.CamOnMyPlayer();
            ViewChanger.instance.ChangeSpawnPos(tmp);
            gameClear = 0;
        }
    }

    public void ChangeSpawn(float x, float y)
    {
        tmp = new Vector3(x, y, 0);
    }
    public void GameStart()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == gameClear)
        {
            PhotonNetwork.LoadLevel(CurrentStage.ToString() + "-" + CurrentChapter.ToString());
        }
    }

    public void ChangeValue(int value)
    {
        Stage[CurrentStage - 1].Chapter[CurrentChapter - 1] -= value;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Stage.Length-1; i++)
        {
            for (int j = 0; j < Stage[i].Chapter.Length; j++)
            {
                Stage2[i].Chapter[j] = Stage[i].Chapter[j];
            }
        }

        GameObject canvas = GameObject.Find("Canvas");
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(canvas);
        if (instance == null)
            instance = this;
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            GameObject temp = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);

        }
    }
}
