using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Emotion : MonoBehaviourPunCallbacks
{
    public int curruntEmotion = 0;
    public GameObject[] m_emoticon;
    public bool isEmoticonOn = false;

    private void Start()
    {
        if(photonView.IsMine)
          GameObject.Find("EmotionButton").GetComponent<EmoticonPannel>().id = photonView.ViewID;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (curruntEmotion != 0)
        {
            isEmoticonOn = true;
            photonView.RPC("EmotionCall", RpcTarget.All, curruntEmotion);
        }

    }

    void CanEmotion()
    {
        isEmoticonOn = false;
    }

    [PunRPC]
    public void EmotionCall(int a)
    {
        curruntEmotion = a;
        GameObject temp = Instantiate(m_emoticon[curruntEmotion] ,this.gameObject.transform);
        if(a != 4)
            Destroy(temp, 2f);

        Invoke("CanEmotion", 2f);
        curruntEmotion = 0;
    }
}
