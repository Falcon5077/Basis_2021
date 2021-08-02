using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Falling_block : MonoBehaviour
{
    Collider ObjectCollider;

    [System.Serializable]
    public enum BoxType
    {
        FallingBox,
        SwtichBox,
        ButtonBox
    }
    public BoxType boxType;

    Falling_block_Time fbT;

    public Vector3 StartPos;
    public Vector3 EndPos;
    public bool dropBox;
    public float dropSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (boxType == BoxType.FallingBox)
        {
            fbT = gameObject.GetComponent<Falling_block_Time>();
        }

        if(boxType == BoxType.SwtichBox)
        {
            StartPos = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (boxType == BoxType.SwtichBox)
        {
            if (dropBox)
            {
                transform.position = Vector3.MoveTowards(transform.position, EndPos, dropSpeed * Time.deltaTime);
            }
            else if (!dropBox)
            {
                transform.position = Vector3.MoveTowards(transform.position, StartPos, dropSpeed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (boxType == BoxType.FallingBox)
        {
            if (other.gameObject.tag.Equals("Ground"))
            {
                fbT.enabled = true;
            }
        }

        if (boxType == BoxType.SwtichBox)
        {
            dropBox = true;
            CancelInvoke("InvokeDropBox");
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (boxType == BoxType.FallingBox)
        {
            if (other.gameObject.tag.Equals("Ground"))
            {
                fbT.setTime = fbT.time;
                fbT.enabled = false;
            }
        }

        if (boxType == BoxType.SwtichBox && dropBox == true)
        {
            Invoke("InvokeDropBox", 1f);
        }
    }

    void InvokeDropBox()
    {
        dropBox = !dropBox;
    }
}
