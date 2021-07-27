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
        if(curruntEmotion != 0)
        {
            isEmoticonOn = true;
            photonView.RPC("EmotionCall", RpcTarget.All, curruntEmotion);
        }

        if (photonView.IsMine && !isEmoticonOn)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                curruntEmotion = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                curruntEmotion = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                curruntEmotion = 3;
            }
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
        GameObject temp = Instantiate(m_emoticon[curruntEmotion], this.gameObject.transform);
        Destroy(temp, 3f);
        Invoke("CanEmotion", 3f);
        curruntEmotion = 0;
    }
}
