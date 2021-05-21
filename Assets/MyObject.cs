using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MyObject : MonoBehaviourPunCallbacks
{
    public GameObject Contents;
    public Text code;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            code = GameObject.Find("Code").GetComponent<Text>();
            PhotonNetwork.LocalPlayer.NickName = "Hello" + Random.Range(0, 9999).ToString();
            code.text = "Room Code : " + PhotonNetwork.CurrentRoom.Name;

        }
        
        GameObject temp = Instantiate(Contents, GameObject.Find("Content").transform);
        temp.name = GetComponent<PhotonView>().Owner.NickName;
        transform.name = GetComponent<PhotonView>().Owner.NickName;
        temp.transform.GetChild(0).GetComponent<Text>().text = GetComponent<PhotonView>().Owner.NickName;
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

