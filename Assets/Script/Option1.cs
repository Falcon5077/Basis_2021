using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option1 : MonoBehaviour
{
    public GameObject OptionPanel;
    public GameObject menuSet;
    public bool buttonOnOff;
    // Start is called before the first frame update
    void Start()
    {

    }
    void update()
    {
        if (Input.GetButtonDown("Option Button"))
            OptionPanel.SetActive(true);
    }
    // Update is called once per frame
    public void Option_Button()
    {
        buttonOnOff = !buttonOnOff;
        OptionPanel.SetActive(buttonOnOff);
    }
}
