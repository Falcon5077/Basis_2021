using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public GameObject OptionPanel;
    public bool buttonOnOff;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Option_Button( )
    {
        buttonOnOff = !buttonOnOff;
        OptionPanel.SetActive(buttonOnOff);
    }
}
