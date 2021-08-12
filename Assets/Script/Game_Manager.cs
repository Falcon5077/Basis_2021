using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

[System.Serializable]
public class StageList
{
    public int[] Chapter;
}

public class Game_Manager : MonoBehaviourPun
{
    //Stage[1].Chapter[0]
    public StageList[] Stage;
    public int CurrentChapter = 1;
    public int CurrentStage = 1;
    public bool CanNextStage = false;

    public int gameClear = 0;

    public static Game_Manager instance;
    public GameObject playerPrefab;
    public static GameObject LocalPlayerInstance;
    SpriteRenderer spriteRenderer;

    public int Key = 0;
    private void Update()
    {
        if(Stage[CurrentStage-1].Chapter[CurrentChapter-1] == 0)
        {
            CanNextStage = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            LoadLobby();
        }
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
            if (CurrentChapter == 4)
            {
                CurrentChapter = 1;
                CurrentStage += 1;
                GameStart();
            }

            ViewChanger.instance.CamOnMyPlayer();
            gameClear = 0;
        }
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
