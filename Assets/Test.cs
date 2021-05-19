using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Button joinButton; // 룸 접속 버튼

    #region MonoBehaviour CallBacks

    void Start()
    {
        string defaultName = "Hello" + Random.Range(0, 9999).ToString();
        PhotonNetwork.NickName = defaultName;
    }

    #endregion

    #region Public Methods

    #endregion
}