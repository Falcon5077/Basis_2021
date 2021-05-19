using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MyObject : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<PhotonView>().Owner.NickName = GetComponentInChildren<Text>().text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
