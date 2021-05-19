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
        GameObject temp = (GameObject)Instantiate(UserPrefab, UserContents.transform);
        temp.GetComponentInChildren<Text>().text = "Hello" + Random.Range(0, 9999).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

