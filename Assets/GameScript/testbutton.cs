using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class testbutton : MonoBehaviourPunCallbacks
{
    GameObject gameManager;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {

            spriteRenderer.color = Color.red;
            if (photonView.IsMine)
            {
                gameManager.GetComponent<Game_Manager>().photonView.RPC("Button_Interaction_1", RpcTarget.All);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            spriteRenderer.color = Color.white;
            if (photonView.IsMine)
            {
                gameManager.GetComponent<Game_Manager>().photonView.RPC("Button_Interaction_0", RpcTarget.All);
            }
        }
    }
}
