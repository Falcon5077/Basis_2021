using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    TextMeshPro textmeshPro;
    void Awake()
    {
        textmeshPro = GetComponent<TextMeshPro>();
        textmeshPro.outlineWidth = 0.2f;
        textmeshPro.outlineColor = new Color32(255, 128, 0, 255);
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Camera.main.transform.position + new Vector3(0, 3, 5);
        transform.parent = Camera.main.transform;
        StartCoroutine("RunFadeOut");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RunFadeOut()
    {
        for( int i = 3; i > 0; i--)
        {
            textmeshPro.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        Destroy(this.gameObject);
    }

}
