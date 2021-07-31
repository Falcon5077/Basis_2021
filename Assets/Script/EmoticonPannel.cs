using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EmoticonPannel : MonoBehaviourPunCallbacks
{
    public bool isPannelOn = false;
    public GameObject Pannel;
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClickEmoticon(int a)
    {
        if (PhotonView.Find(id).transform.gameObject.GetComponent<Emotion>().isEmoticonOn == false)
             PhotonView.Find(id).transform.gameObject.GetComponent<Emotion>().curruntEmotion = a;
    }

    public void OnOffPannel()
    {
        isPannelOn = !isPannelOn;
        Pannel.SetActive(isPannelOn);
    }
}
