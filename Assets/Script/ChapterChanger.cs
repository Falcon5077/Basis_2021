using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using CameraWorks;

public class ChapterChanger : MonoBehaviourPunCallbacks
{
    public float nextX = 0;
    public float nextY = 0;

    Game_Manager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<Game_Manager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(gameManager.CanNextStage)
        {
            if (other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                gameManager.photonView.RPC("rpcPass", RpcTarget.AllBuffered);
                other.gameObject.GetComponent<CameraWork>().enabled = false;
                other.gameObject.transform.position = new Vector2(nextX, nextY);

                gameManager.photonView.RPC("NextChapter", RpcTarget.AllBuffered); 
            }
        }
    }


}
