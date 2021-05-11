using Global;
using SceneSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TaskSystem
{
    public class TaskResultScreen : MonoBehaviour
    {
        [SerializeField]
        private GameObject taskSuccessScreen;

        [SerializeField]
        private GameObject taskFailedScreen;

        [SerializeField]
        private float sceneTransitionTime = 10;

        void Awake()
        {
            // Subscribe the screen functions
            TaskController.TaskEndedAction += TaskEnded;

            // Disable all screens
            DisableScreens();

            // Subscrives the function for disabling the result screen on load
            SceneManager.sceneLoaded += DisableScreensBySceneLoaded;
        }

        /// <summary>
        /// Is called when a task has ended so that it may enable the result screen
        /// </summary>
        /// <param name="nextOverworldIndex"></param>
        /// <param name="state"></param>
        void TaskEnded(ToolType nextOverworldIndex, bool state)
        {
            print("Task ended");

            if (state)// Success
            {
                EnableScreen(taskSuccessScreen);
            }
            else// Failed
            {
                EnableScreen(taskFailedScreen);
            }

            StartCoroutine("CountdownSwitchToOverworld", nextOverworldIndex);
        }

        /// <summary>
        /// Enables the given gameobject
        /// </summary>
        void EnableScreen(GameObject screenObject)
        {
            if (screenObject == null)
                return;

            screenObject.SetActive(true);
        }

        /// <summary>
        /// Disables all screens
        /// </summary>
        void DisableScreens()
        {
            taskSuccessScreen.SetActive(false);
            taskFailedScreen.SetActive(false);
        }

        /// <summary>
        /// Disables all screens when "SceneManager.sceneLoaded" is called (Parameters not important)
        /// </summary>
        /// <param name="scene">Not important</param>
        /// <param name="mode">Not important</param>
        void DisableScreensBySceneLoaded(Scene scene, LoadSceneMode mode)
        {
            print("Disabled Screens by loaded a new scene");
            DisableScreens();
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
}