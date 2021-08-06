using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public GameObject OptionPanel;
    public bool buttonOnOff;
    public void Option_On()
    {
        buttonOnOff = true;
        OptionPanel.SetActive(buttonOnOff);
    }

    public void Option_Off()
    {
        buttonOnOff = false;
        OptionPanel.SetActive(buttonOnOff);
    }
}
