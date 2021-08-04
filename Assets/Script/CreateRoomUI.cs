using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.EventSystems;

public class CreateRoomUI : MonoBehaviourPun
{

    [SerializeField]
    private List<Button> maxPlayerCountButtons;

    private CreateGameRoomData roomData;

    void Start()
    {
        roomData = new CreateGameRoomData() {maxPlayerCount = 4 };

    }

    private void Update()
    {

    }
    #region MonoBehaviour CallBacks
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true; 
    }
    #endregion

    public void UpdateMaxPlayerCount(int count)
    {
        roomData.maxPlayerCount = count;

        for(int i = 0; i < maxPlayerCountButtons.Count; i++)
        {
            if (i == count - 1)
            {
                maxPlayerCountButtons[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
            
            else
            {
                maxPlayerCountButtons[i].image.color = new Color(1f, 1f, 1f, 0f);
            }
        }

        GameObject.Find("UserManager").GetComponent<UserManager>().ChangeMPC(count-1);
    }
    public void GameStart()
    {
        //SceneManager.LoadScene("Player");   
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        PhotonNetwork.LoadLevel("1-1");
    }
    public void LeaveRoom()
    {
        GameObject userManager = GameObject.Find("UserManager");
        foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
        {
            if (p.ActorNumber != userManager.GetPhotonView().OwnerActorNr)
            {
                if(userManager.GetPhotonView().IsMine)
                    userManager.GetPhotonView().TransferOwnership(p.ActorNumber);
                break;

            }
        }


        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
        PhotonNetwork.LeaveRoom();
        Invoke("OnLeftRoom", 0.3f);
        //PhotonNetwork.LoadLevel("Lobby");
    }

    public void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }
}

public class CreateGameRoomData
{
    public int maxPlayerCount;
}