using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UserManager : MonoBehaviourPunCallbacks
{
    public GameObject UserContents;
    public GameObject UserPrefab;

    public GameObject[] MasterUI;
    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = PhotonNetwork.Instantiate(UserPrefab.name, Vector3.zero, Quaternion.identity,0);

        for (int i = 0; i < 5; i++)
        {
            MasterUI[i] = GameObject.Find("Button" + (i + 1).ToString());

        }

    }


    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (photonView.Owner.IsMasterClient)
            {
                for (int i = 0; i < 5; i++)
                {
                    MasterUI[i].GetComponent<Button>().interactable = true;

                }
            }
        }
    }

    [PunRPC]
    void MaxPlayerCount(int a)
    {

        for (int i = 0; i < 4; i++)
        {
            if (i == a)
            {
                MasterUI[i].GetComponent<Button>().image.color = new Color(1f, 1f, 1f, 1f);
            }

            else
            {
                MasterUI[i].GetComponent<Button>().image.color = new Color(1f, 1f, 1f, 0f);
            }
        }

    }

    public void ChangeMPC(int a)
    {
        photonView.RPC("MaxPlayerCount", RpcTarget.AllBuffered,a);
    }
}

