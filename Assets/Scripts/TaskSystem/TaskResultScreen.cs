using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class TaskResultScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject TaskFailedScreen;

    [SerializeField]
    private GameObject TaskSuccesScreen;

    [SerializeField]
    private float AmountOfSecondsToScreen;

    [SerializeField]
    private float sceneTransitionTime = 10;

    private void Start()
    {
        TaskFailedScreen.SetActive(false);
        TaskSuccesScreen.SetActive(false);
    }

    public void ShowResultScreen(ToolType nextOverworldIndex, bool taskSucceeded)
    {
        //code voor het laten zien van de taskresultscreen

        //pre-load de volgende scene hier via async

        if(taskSucceeded)
        {
            ShowTaskSuccesScreen(nextOverworldIndex);
        }
        else
        {
            ShowTaskFailedScreen(nextOverworldIndex);
        }
    }

    private void ShowTaskFailedScreen(ToolType nextOverworldIndex)
    {
        TaskFailedScreen.SetActive(true);
        StartCoroutine("CountdownSwitchToOverworld", nextOverworldIndex);
    }

    private void ShowTaskSuccesScreen(ToolType nextOverworldIndex)
    {
        TaskFailedScreen.SetActive(true);
        StartCoroutine("CountdownSwitchToOverworld", nextOverworldIndex);
    }

    /// <summary>
    /// Switches to the next overworld scene after a given amount of time
    /// </summary>
    /// <param name="nextOverworldIndex"></param>
    /// <returns></returns>
    IEnumerator CountdownSwitchToOverworld(ToolType nextOverworldIndex)
    {
        yield return new WaitForSeconds(sceneTransitionTime);

        SceneController.SwitchSceneToOverWorld(nextOverworldIndex);
    }

}
