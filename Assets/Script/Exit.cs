using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// change name from OnMouseDown_SwitchScene to Exit
public class Exit : MonoBehaviour
{
    public string sceneName;

    public void OnMouseDown()
    {

        SceneManager.LoadScene(sceneName);
    }
}
