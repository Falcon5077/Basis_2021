using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;
using Photon.Pun;

public class BoxScript : MonoBehaviour
{
    Collider ObjectCollider;
    public buttonBox myButton;
    public Pickup myKey;

    [System.Serializable]   // 인스펙터 창에서 볼수있게 하는 설정
    public enum BoxType
    {
        none,
        DestroyBox,
        MoveBox,
        JumpingBox,
        TeleportBox,
        ButtonMoveBox,
        ButtonDestroyBox
    }

    [Header(" - Must Set Box Type")]
    public BoxType boxType;

    Falling_block_Time fbT;

    public Vector3 StartPos;

    [Header(" - MoveBox Option")]
    public Vector3 EndPos;
    public float moveSpeed;
    private bool isBoxMoving;

    [Header(" - JumpingBox Option")]
    public float jumpForce;

    [Header(" - TeleportBox Option")]
    public Transform tpTarget;

    // Start is called before the first frame update
    void Start()
    {
        if (boxType == BoxType.DestroyBox || boxType == BoxType.ButtonMoveBox) // 주성
        {
            fbT = gameObject.GetComponent<Falling_block_Time>();
        }

        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (boxType == BoxType.MoveBox || boxType == BoxType.ButtonMoveBox)//주성
        {
            if (isBoxMoving)
            {
                transform.position = Vector3.MoveTowards(transform.position, EndPos, moveSpeed * Time.deltaTime);
            }
            else if (!isBoxMoving)
            {
                transform.position = Vector3.MoveTowards(transform.position, StartPos, moveSpeed * Time.deltaTime);
            }
        }

        if (boxType == BoxType.ButtonMoveBox)//주성
        {
            if (myButton != null)
            {
                if (myButton.Button_pressed == false)
                {
                    isBoxMoving = false;
                }
                else if (myButton.Button_pressed == true)
                {
                    isBoxMoving = true;
                }
            }
            
            if (myKey != null)
            {
                if (myKey.hasKey == true)
                {
                    isBoxMoving = true;
                }
                else if (myKey.hasKey == false)
                {
                    isBoxMoving = false;
                }
            }
        }

        if(boxType == BoxType.ButtonDestroyBox)
        {
            if (myButton != null)
            {
                if (myButton.Button_pressed == false)
                {
                    GetComponent<BoxCollider2D>().enabled = true;
                    GetComponent<SpriteRenderer>().enabled = true;
                }
                else if (myButton.Button_pressed == true)
                {
                    GetComponent<BoxCollider2D>().enabled = false;
                    GetComponent<SpriteRenderer>().enabled = false;
                }
            }

            if (myKey != null)
            {
                if (myKey.hasKey == true)
                {
                    GetComponent<BoxCollider2D>().enabled = false;
                    GetComponent<SpriteRenderer>().enabled = false;
                }
                else if (myKey.hasKey == false)
                {
                    GetComponent<BoxCollider2D>().enabled = true;
                    GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if( boxType == BoxType.ButtonMoveBox)
        {
            if (myButton != null)
            {
                if (myButton.Button_pressed == false)
                    other.transform.position = this.transform.position + new Vector3(0, 0.8f, 0);
            }
            if (myKey != null)
            {
                if (myKey.hasKey == false)
                    other.transform.position = this.transform.position + new Vector3(0, 0.8f, 0);
            }
        }
        
        if (boxType == BoxType.DestroyBox)
        {
            if (other.gameObject.tag.Equals("Ground"))
            {
                if (fbT != null)
                {
                    CancelInvoke("InvokeStopShake");
                    fbT.enabled = true;
                }
            }
        }

        if (boxType == BoxType.MoveBox)
        {
            isBoxMoving = true;
            CancelInvoke("InvokeisBoxMoving");
        }
        if (boxType == BoxType.JumpingBox)
        {
            if (other.gameObject.tag.Equals("Ground"))
            {
                other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                if(other.gameObject.GetComponent<Player.PlayerController>() != null)
                    other.gameObject.GetComponent<Player.PlayerController>().isJumping = false;
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
        /*
        if (boxType == BoxType.MoveBox || boxType == BoxType.ButtonMoveBox)
        {
            if (other.transform.GetComponent<PhotonView>().IsMine)
            {
                other.transform.parent = null;
                DontDestroyOnLoad(other.gameObject);
            }
        }
        */
        if (boxType == BoxType.DestroyBox)
        {
            if (other.gameObject.tag.Equals("Ground"))
            {
                Invoke("InvokeStopShake", 0.2f);
            }
        }

        if (boxType == BoxType.MoveBox && isBoxMoving == true)
        {
            Invoke("InvokeisBoxMoving", 0.75f);
        }
    }
    void InvokeStopShake()
    {
        fbT.setTime = fbT.time;
        fbT.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        fbT.enabled = false;
    }

    void InvokeisBoxMoving()
    {
        isBoxMoving = !isBoxMoving;
    }
}
