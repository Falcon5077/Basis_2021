using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Falling_block_Time : MonoBehaviour
{
    public float time = 2.0f;
    [SerializeField] 
    public float setTime = 2.0f;
    public GameObject FallingBlock;
    // Start is called before the first frame update
    void Start()
    {
        FallingBlock = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (setTime > 0)
            setTime -= Time.deltaTime;
        else if (setTime <=0 && setTime > -1)
        {
            Destroy(FallingBlock);
        }
    }

}
