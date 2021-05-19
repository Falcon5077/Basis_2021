using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MyObject : MonoBehaviourPunCallbacks
{
    public GameObject Contents;
    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
            PhotonNetwork.LocalPlayer.NickName = "Hello" + Random.Range(0, 9999).ToString();
        
        GameObject temp = Instantiate(Contents, GameObject.Find("Content").transform);
        temp.name = GetComponent<PhotonView>().Owner.NickName;
        transform.name = GetComponent<PhotonView>().Owner.NickName;
        temp.transform.GetChild(0).GetComponent<Text>().text = GetComponent<PhotonView>().Owner.NickName;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

