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
    private GameObject ShowGame, ShowHelp;

    [SerializeField]
    private bool enabledCanvas = true;

    private void Start()
    {
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
        if (enabledCanvas)
        {
            ShowGame.GetComponent<Canvas>().enabled = !active;

        }
        else
        {
            ShowGame.SetActive(!active);
        }

        ShowHelp.SetActive(active);
    }
}
