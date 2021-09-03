using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class OutBound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            other.gameObject.transform.position = other.gameObject.GetComponent<PlayerController>().spawnPos;
        }

    }
}