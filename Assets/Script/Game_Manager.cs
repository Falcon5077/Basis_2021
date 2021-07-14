using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class Game_Manager : MonoBehaviourPun
{
    public static Game_Manager instance;
    public GameObject playerPrefab;
    public static GameObject LocalPlayerInstance;


    public int Key = 0;



    [PunRPC]
    public void GetKey()
    {
        Key++;
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
