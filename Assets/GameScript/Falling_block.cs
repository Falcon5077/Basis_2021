using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;

public class Falling_block : MonoBehaviour
{
    Collider ObjectCollider;

    [System.Serializable]
    public enum BoxType
    {
        none,
        FallingBox,
        SwtichBox,
        JumpingBox,
        TeleportBox,
        ButtonBox
    }
    public BoxType boxType;

    Falling_block_Time fbT;

    public Vector3 StartPos;
    public Vector3 EndPos;
    public bool dropBox;
    public float dropSpeed;

    public float jumpForce;

    public Transform tpTarget;

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
        if (boxType == BoxType.JumpingBox)
        {
            if (other.gameObject.tag.Equals("Ground"))
            {
                other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                if(other.gameObject.GetComponent<CatController>() != null)
                    other.gameObject.GetComponent<CatController>().isJumping = false;
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpForce);
            }
        }

        if (boxType == BoxType.TeleportBox)
        {
            if (other.gameObject.tag.Equals("Ground"))
            {
                other.gameObject.transform.position = tpTarget.position;
            }
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
