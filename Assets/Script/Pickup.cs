using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Pickup : MonoBehaviourPunCallbacks
{
    public int KeyValue = 0;
    public int resetValue;
    public bool hasKey = false;

    private void Start()
    {
        resetValue = KeyValue;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.tag.Equals("Key")) {
            Game_Manager.instance.ChangeValue(KeyValue);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            hasKey = true;
            //Destroy(this.gameObject);
        }
    }
} 
