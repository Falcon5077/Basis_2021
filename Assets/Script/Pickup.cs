using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Pickup : MonoBehaviourPunCallbacks
{
    public int GameClear = 0;

    GameObject gameManager;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Key"))
        {
            Destroy(other.gameObject);
            if (photonView.IsMine)
            {
                gameManager.GetComponent<Game_Manager>().photonView.RPC("GetKey", RpcTarget.All);
            }
        }

        if (other.gameObject.tag.Equals("Exit"))
        {
            if(gameManager.GetComponent<Game_Manager>().Key >= 1)
            {
                if (photonView.IsMine)
                {
                    //Destroy(other.gameObject);
                    //transform.position = Vector3.zero;
                    LeaveRoom();
                }
                //transform.position = new Vector3(20, -10, 0);
            }
        }
    }

    public void LeaveRoom()
    {
        Game_Manager.instance.ChangeScene();
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        //PhotonNetwork.Destroy(this.gameObject);

    }
} 
