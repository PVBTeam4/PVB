using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class TaskResultScreen : MonoBehaviour
{
    // 'failed' screen canvas gameobject that corresponds with the task that this script is used on
    [SerializeField]
    private GameObject TaskFailedScreen;

    // 'succes' screen canvas gameobject that corresponds with the task that this script is used on
    [SerializeField]
    private GameObject TaskSuccesScreen;

    // The amount of time before the scene is switched in seconds
    [SerializeField]
    private float sceneTransitionTime = 10;

    /// <summary>
    /// Sets both ending screens to a default value of 'false'
    /// Adds the 'ShowResultScreen' function to the 'EndOfTask' event of the TaskController
    /// </summary>
    private void Start()
    {
        TaskFailedScreen.SetActive(false);
        TaskSuccesScreen.SetActive(false);

        GameManager.Instance.TaskController.EndOfTask += ShowResultScreen;
    }

    /// <summary>
    /// Enables the result screen based on information from the TaskController
    /// </summary>
    /// <param name="nextOverworldIndex"></param>
    /// <param name="taskSucceeded"></param>
    public void ShowResultScreen(ToolType nextOverworldIndex, bool taskSucceeded)
    {
        TaskSuccesScreen.SetActive(taskSucceeded);
        TaskFailedScreen.SetActive(!taskSucceeded);

        StartCoroutine("CountdownSwitchToOverworld", nextOverworldIndex);

        Debug.Log("Switching to the next scene in " + sceneTransitionTime + " seconds.");
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
