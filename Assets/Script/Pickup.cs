using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Pickup : MonoBehaviourPunCallbacks
{
    public int KeyValue = 0;
    Game_Manager gameManager;

    private void Start()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<Game_Manager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.tag.Equals("Key")) {
            //gameManager.ChangeValue(KeyValue);
            Game_Manager.instance.ChangeValue(KeyValue);
            Destroy(this.gameObject);
        }
    }
} 
