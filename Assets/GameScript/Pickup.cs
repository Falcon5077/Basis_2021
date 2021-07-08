using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pickup : MonoBehaviour
{
    private int GameClear = 0;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag.Equals("Key"))
        {
            Destroy(other.gameObject);
            GameClear ++;
        }

        if (other.gameObject.tag.Equals("Exit"))
        {
            if(GameClear == 1)
            {
                Destroy(other.gameObject);
                transform.position = new Vector3(20, -10, 0);

            }
        }
    }
} 
