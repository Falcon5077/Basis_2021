using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UserManager : MonoBehaviourPunCallbacks
{
    public GameObject UserContents;
    public GameObject UserPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = PhotonNetwork.Instantiate(UserPrefab.name, Vector3.zero, Quaternion.identity,0);
        temp.name = GetComponent<PhotonView>().Owner.NickName;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

