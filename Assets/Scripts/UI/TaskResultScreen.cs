using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace TaskSystem
{
    public class TaskResultScreen : MonoBehaviour
    {
        [SerializeField]
        private GameObject taskSuccessScreen;

        [SerializeField]
        private GameObject taskFailedScreen;

        void Awake()
        {
            // Subscribe the screen functions
            TaskController.TaskEndedAction += TaskEnded;

            // Disable all screens
            DisableScreens();

            // 
            SceneManager.sceneLoaded += DisableScreensBySceneLoaded;
        }

        void TaskEnded(bool state)
        {
            if (state)// Success
            {
                EnableScreen(taskSuccessScreen);
            }
            else// Failed
            {
                EnableScreen(taskFailedScreen);
            }
        }

        /// <summary>
        /// Enables the given gameobject
        /// </summary>
        void EnableScreen(GameObject screenObject)
        {
            if (screenObject != null)
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
    }
}