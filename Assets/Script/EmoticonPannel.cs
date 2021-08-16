using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EmoticonPannel : MonoBehaviourPunCallbacks
{
    public bool isPannelOn = false;
    public GameObject Pannel;
    public int id;
    public GameObject CountDown;

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            CountDown.SetActive(true);
        }
    }

    public void ClickEmoticon(int a)
    {
        if (PhotonView.Find(id).transform.gameObject.GetComponent<Emotion>().isEmoticonOn == false)
        //if (PhotonView.Find(id).transform.gameObject.GetComponent<Emotion>().count <= 3)
            PhotonView.Find(id).transform.gameObject.GetComponent<Emotion>().curruntEmotion = a;
    }

    public void OnOffPannel()
    {
        isPannelOn = !isPannelOn;
        Pannel.SetActive(isPannelOn);
    }
}
