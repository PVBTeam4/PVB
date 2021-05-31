using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///this class is to enable/disable the tutorial and the game
/// </summary>
public class Tutorial : MonoBehaviour
{
    [SerializeField]
    //object that need to be Disabled
    private GameObject DisableObject;
    [SerializeField]
    //object that need to be Enabled
    private GameObject EnableObject;
    [SerializeField]
    //PressAnyKey default is false but if enabled it wil allow any key to be pressed
    private bool PressAnyKey = false;

 
    void Update()
    {
        //check if PressAnyKey is true
        if (PressAnyKey)
        {
            if (UnityEngine.Input.anyKeyDown)
            {   
                EnableDisableObject();
                Time.timeScale = 1; 
            }
        }
        else
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.H))
            {
                EnableDisableObject();
                Time.timeScale = 0;
            }

        }
     
    }
    /// <summary>
    /// SetActive to enable/disable object
    /// </summary>
    private void EnableDisableObject()
    {
        EnableObject.SetActive(true);
        DisableObject.SetActive(false);
    }
}
