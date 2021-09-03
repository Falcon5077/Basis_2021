using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UserManager : MonoBehaviourPunCallbacks
{
    public GameObject UserContents;
    public GameObject UserPrefab;

    public GameObject StartBtn;
    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = PhotonNetwork.Instantiate(UserPrefab.name, Vector3.zero, Quaternion.identity,0);

        for (int i = 0; i < 5; i++)
        {
            StartBtn = GameObject.Find("Start");

        }

    }


    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (photonView.Owner.IsMasterClient)
            {
                StartBtn.GetComponent<Button>().interactable = true;
            }
        }
    }
}

