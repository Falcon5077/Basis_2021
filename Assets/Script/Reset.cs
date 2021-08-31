using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    Pickup Key;
    BoxScript Obj;
    public Vector3 StartPos;

    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;

        if (GetComponent<Pickup>() != null)
        {
            Key = GetComponent<Pickup>();
        }
        else if (GetComponent<BoxScript>() != null)
        {
            Obj = GetComponent<BoxScript>();
        }
    }

    public void PositionReset()
    {
        if (Key != null)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
            Key.KeyValue = Key.resetValue;
            Key.hasKey = false;
        }
        if (Obj != null)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
            StartPos = Obj.StartPos;
        }

        transform.position = StartPos;

    }


    // Update is called once per frame
    void Update()
    {

    }
}
