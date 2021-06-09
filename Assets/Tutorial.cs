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
    //object that can be be Disabled or Enabled
    private GameObject ShowGame, ShowHelp, TaskResultScreens;

    private void Start()
    {   
        TaskResultScreens = GameObject.Find("Task Result Screens");
        EnableDisableObject(true);
    }
    void Update()
    {
        //check if ShowTutorial is true
        if (ShowHelp.activeSelf)
        {
            if (UnityEngine.Input.anyKeyDown)
            {
                EnableDisableObject(false);
                Time.timeScale = 1;
            }
        }
        else
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.H))
            {
                EnableDisableObject(true);
                Time.timeScale = 0;
            }

        }

    }
    /// <summary>
    /// SetActive to enable/disable object
    /// </summary>
    private void EnableDisableObject(bool active)
    {
        ShowGame.GetComponent<Canvas>().enabled = !active;
        ShowHelp.SetActive(active);

        if(TaskResultScreens != null)
            TaskResultScreens.SetActive(!active);
    }
}
