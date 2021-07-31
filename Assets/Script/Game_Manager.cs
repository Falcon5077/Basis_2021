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
    public int Button_pressed = 0;

    private void Update()
    {
        if(Stage[CurrentStage-1].Chapter[CurrentChapter-1] == 0)
        {
            CanNextStage = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {

            PhotonNetwork.LeaveRoom();
            Invoke("OnLeftRoom", 0.3f);
        }
    }
public void OnLeftRoom()
{
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
            ViewChanger.instance.CamOnMyPlayer();
            CanNextStage = false;
            CurrentChapter += 1;
            gameClear = 0;
        }

    }
    public void ChangeValue(int value)
    {
        Stage[CurrentStage - 1].Chapter[CurrentChapter - 1] -= value;
    }

    [PunRPC]
    public void GetKey()
    {
        Key++;
    }

    [PunRPC]
    public void Button_Interaction_1()
    {
        Button_pressed++;
    }
   
    [PunRPC]
    public void Button_Interaction_0()
    {
        Button_pressed--;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
            instance = this;
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            //Debug.LogFormat("We are Instantiating LocalPlayer from {0}", Application.loadedLevelName);
            GameObject temp = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
            DontDestroyOnLoad(temp);
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
        }
    }

    public void ChangeScene()
    {
        Invoke("test", 1f);
    }

    public void test()
    {
        SceneManager.LoadScene("Lobby");
    }
}
