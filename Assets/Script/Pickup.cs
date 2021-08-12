using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Pickup : MonoBehaviourPunCallbacks
{
    public int KeyValue = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.tag.Equals("Key")) {
            Game_Manager.instance.ChangeValue(KeyValue);
            Destroy(this.gameObject);
        }
    }
} 
