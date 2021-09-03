using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MyObject : MonoBehaviourPunCallbacks
{
    public GameObject Contents;
    public GameObject[] MasterUI;
    public Text code;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            code = GameObject.Find("Code").GetComponent<Text>();
            PhotonNetwork.LocalPlayer.NickName = "Hello" + Random.Range(0, 9999).ToString();
            code.text = "Room Code : " + PhotonNetwork.CurrentRoom.Name;

            if (photonView.Owner.IsMasterClient)
            {
               /* for (int i = 0; i < 5; i++)
                {
                    MasterUI[i] = GameObject.Find("Button" + (i+1).ToString());
                    MasterUI[i].GetComponent<Button>().interactable = true;

                }*/
            }

        }
        
        GameObject temp = Instantiate(Contents, GameObject.Find("Content").transform);
        temp.name = GetComponent<PhotonView>().Owner.NickName;
        transform.name = GetComponent<PhotonView>().Owner.NickName;
        temp.transform.GetChild(0).GetComponent<Text>().text = GetComponent<PhotonView>().Owner.NickName;

        if(photonView.IsMine)
        {
            temp.GetComponent<Image>().color = new Color(60/255, 1f, 80/255, 1f);
        }
    }

    private void OnDestroy()
    {
        Destroy(GameObject.Find(GetComponent<PhotonView>().Owner.NickName));
    }


    // Update is called once per frame
    void Update()
    {

    }
}

