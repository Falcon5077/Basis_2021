using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite defaultIMG;
    public Sprite nextIMG;

    public float InvokeDelay;
    private bool pushed;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        pushed = true;
        spriteRenderer.sprite = nextIMG;
    }
    void OnCollisionStay2D(Collision2D other)
    {
        pushed = true;
        spriteRenderer.sprite = nextIMG;
    }
    void OnCollisionExit2D(Collision2D other)
    {
        pushed = false;
        Invoke("InvokeDefaultIMG", InvokeDelay);
    }


    void InvokeDefaultIMG()
    {
        if (pushed == true)
            return;

        spriteRenderer.sprite = defaultIMG;
    }
}
