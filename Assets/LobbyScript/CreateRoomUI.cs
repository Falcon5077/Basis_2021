using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class CreateRoomUI : MonoBehaviour
{
    [SerializeField]
    private List<Button> maxPlayerCountButtons;

    private CreateGameRoomData roomData;

    void Start()
    {
        roomData = new CreateGameRoomData() {maxPlayerCount = 4 }; 
    }

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
    }
    public void GameStart()
    {
        SceneManager.LoadScene("Game");
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();

        SceneManager.LoadScene("Lobby");
        //PhotonNetwork.LoadLevel("Lobby");
    }
}

public class CreateGameRoomData
{
    public int maxPlayerCount;
}