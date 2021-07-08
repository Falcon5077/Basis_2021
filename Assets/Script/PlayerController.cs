using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 3.0f;
    private Vector3 moveDirction = Vector3.zero;

// Update is called once per frame
void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");

        moveDirction = new Vector3(x, 0, 0);

        transform.position += moveDirction * moveSpeed * Time.deltaTime;
    }

}
