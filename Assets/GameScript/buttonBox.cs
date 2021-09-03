using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class buttonBox : MonoBehaviourPunCallbacks
{
    SpriteRenderer spriteRenderer;
    public Sprite ButtonUp;
    public Sprite ButtonDown;

    private bool pushed;

    public bool Button_pressed;

    [PunRPC]
    public void Button_Interaction_1()
    {
        Button_pressed = true;
    }

    [PunRPC]
    public void Button_Interaction_0()
    {
        Button_pressed = false;
    }


    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        pushed = true;
        spriteRenderer.sprite = ButtonDown;
        Button_Interaction_1();
    }
    void OnCollisionStay2D(Collision2D other)
    {
        pushed = true;
        spriteRenderer.sprite = ButtonDown;
        Button_Interaction_1();
    }
    void OnCollisionExit2D(Collision2D other)
    {
        pushed = false;
        Invoke("InvokeButtonUp", 0.25f);
    }


    void InvokeButtonUp()
    {
        if (pushed == true)
            return;

        spriteRenderer.sprite = ButtonUp;
        Button_Interaction_0();
    }
}