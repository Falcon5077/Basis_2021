using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraWorks;


public class ViewChanger : MonoBehaviour
{
    static public ViewChanger instance;
    public List<GameObject> Players = new List<GameObject>();

    public int index = 0; // 0 = me 1,2,3 = players, 4 = every
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CamOnMyPlayer()
    { 
         if (Players[0] != null) Players[0].GetComponent<CameraWork>().enabled = true;
    }

    public void camOnOff(int a,bool b)
    {
        for(int i = 0; i < 4; i++)
        {
            if (Players[i] != null && i == a)
                Players[i].GetComponent<CameraWork>().enabled = b;
            else if(Players[i] != null)
                Players[i].GetComponent<CameraWork>().enabled = !b;
        }
    }

    public void ViewChange()
    {
        if (++index >= 5)
            index = 0;
        if (Players.Count <= index)
            index = 4;

        switch(index)
        {
            case 0:
                if (Players[0] != null) Players[0].GetComponent<CameraWork>().enabled = true;
                break;

            case 1:
                if (Players[0] != null) Players[0].GetComponent<CameraWork>().enabled = false;
                if (Players[1] != null) Players[1].GetComponent<CameraWork>().enabled = true;
                break;

            case 2:
                if (Players[1] != null) Players[0].GetComponent<CameraWork>().enabled = false;
                if (Players[2] != null) Players[1].GetComponent<CameraWork>().enabled = true;
                break;

            case 3:
                if (Players[2] != null) Players[0].GetComponent<CameraWork>().enabled = false;
                if (Players[3] != null) Players[1].GetComponent<CameraWork>().enabled = true;
                break;

            case 4:
                if(Players.Count >= 1) Players[0].GetComponent<CameraWork>().enabled = true;
                if (Players.Count >= 2) Players[1].GetComponent<CameraWork>().enabled = true;
                if (Players.Count >= 3) Players[2].GetComponent<CameraWork>().enabled = true;
                if (Players.Count >= 4) Players[3].GetComponent<CameraWork>().enabled = true;
                break;

            case 5:
                index = 0;
                break;
            }
    }
}
