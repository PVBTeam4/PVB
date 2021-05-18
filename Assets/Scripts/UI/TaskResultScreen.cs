using Global;
using SceneSystem;
using System.Collections;
using TaskSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class TaskResultScreen : MonoBehaviour
    {
        [SerializeField]
        private GameObject taskSuccessScreen;

        [SerializeField]
        private GameObject taskFailedScreen;

        [SerializeField]
        private float sceneTransitionTime = 10;

        private void Awake()
        {
            // Subscribe the screen functions
            TaskController.TaskEndedAction += TaskEnded;

            // Disable all screens
            DisableScreens();
        }

        /// <summary>
        /// Is called when a task has ended so that it may enable the result screen
        /// </summary>
        /// <param name="nextOverWorldIndex"></param>
        /// <param name="isTaskSuccess"></param>
        private void TaskEnded(ToolType nextOverWorldIndex, bool isTaskSuccess)
        {
            Debug.Log("Task Ended Test");
            if (isTaskSuccess)// Success
            {
                EnableScreen(taskSuccessScreen);
            }
            else// Failed
            {
                EnableScreen(taskFailedScreen);
            }

            StartCoroutine("CountdownSwitchToOverworld", nextOverWorldIndex);
        }

        /// <summary>
        /// Enables the given gameobject
        /// </summary>
        private void EnableScreen(GameObject screenObject)
        {
            if (screenObject == null)
                return;

            screenObject.SetActive(true);
        }

        /// <summary>
        /// Disables all screens
        /// </summary>
        private void DisableScreens()
        {
            taskSuccessScreen.SetActive(false);
            taskFailedScreen.SetActive(false);
        }

        /// <summary>
        /// Disables all screens when "SceneManager.sceneLoaded" is called (Parameters not important)
        /// </summary>
        /// <param name="scene">Not important</param>
        /// <param name="mode">Not important</param>
        private void DisableScreensBySceneLoaded(Scene scene, LoadSceneMode mode)
        {
            print("Disabled Screens by loaded a new scene");
            DisableScreens();
        }

        /// <summary>
        /// Switches to the next overworld scene after a given amount of time
        /// </summary>
        /// <param name="nextOverworldIndex"></param>
        /// <returns></returns>
        private IEnumerator CountdownSwitchToOverworld(ToolType nextOverworldIndex)
        {
            yield return new WaitForSeconds(sceneTransitionTime);
            SceneController.SwitchSceneToOverWorld(nextOverworldIndex);
        }


    }
}