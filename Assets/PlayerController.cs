using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private Rigidbody2D rb;
    private bool moveLeft, moveRight;

    private float currentheight, travel, previousheight;

    public void MoveLeft()
    {
        moveLeft = true;
    }

    public void MoveRight()
    {
        moveRight = true;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 3.0f;
        moveLeft = moveRight = false;

        previousheight = transform.position.y;
    }

    public void StopMoving()
    {
        rb.velocity = new Vector2(0f, 0f);
        moveLeft = moveRight = false;
    }

    private void Update()
    {
        ;
    }

    void FixedUpdate()
    {
        if (rb.velocity.y <= 0.0001f)
        {
            if (moveLeft)
            {
                Debug.Log("left");
                rb.velocity = new Vector2(-moveSpeed, 0f);
            }
            else if (moveRight)
            {
                Debug.Log("right");
                rb.velocity = new Vector2(moveSpeed, 0f);
            }
        } else
        {
            Debug.Log("not moving" + rb.velocity.y.ToString());
        }
    }

}