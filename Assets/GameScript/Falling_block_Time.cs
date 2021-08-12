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

    public float dis = 0.1f;
    public bool way = true;
    // Start is called before the first frame update
    void Start()
    {
        FallingBlock = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (setTime > 0)
        {
            setTime -= Time.deltaTime;
            if (setTime <= 1.5f)
            {
                if (way == true)
                    dis += 0.25f;
                if (way == false)
                    dis -= 0.25f;

                if (dis >= 1)
                    way = false;
                if (dis <= -1)
                    way = true;

                transform.rotation = Quaternion.Euler(new Vector3(0, 0, (3 - setTime) * dis));
            }
        }
        else if (setTime <= 0 && setTime > -1)
        {
            Destroy(FallingBlock);
        }
    }

}
