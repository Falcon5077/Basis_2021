﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using CameraWorks;

public class ChapterChanger : MonoBehaviourPun
{
    public float nextX = 0;
    public float nextY = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(Game_Manager.instance.CanNextStage)
        {
            if (other.gameObject.GetComponent<PhotonView>().IsMine && other.gameObject.GetComponent<CameraWork>().enabled == true)
            {
                Game_Manager.instance.tmp = new Vector2(nextX, nextY);
                Game_Manager.instance.photonView.RPC("rpcPass", RpcTarget.AllBuffered);
                other.gameObject.GetComponent<CameraWork>().enabled = false;
                other.gameObject.transform.position = new Vector2(nextX, nextY);
                Game_Manager.instance.photonView.RPC("NextChapter", RpcTarget.AllBuffered);                
            }
        }

        if(nextX == 7777)
        {
            Game_Manager.instance.LoadLobby();

        }
    }



}
