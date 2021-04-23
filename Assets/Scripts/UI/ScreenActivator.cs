using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TaskSystem
{
    public class ScreenActivator : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private bool ActivateOnWinState = true;

        /// <summary>
        /// 
        /// </summary>
        private Action<bool> screenActivationAction;

        private Canvas canvasComponent;

        void Awake()
        {
            // Get the Canvas component
            canvasComponent = gameObject.GetComponent<Canvas>();

            // Subscribe the screen functions
            TaskController.TaskEndedAction += EnableScreen;
            TaskController.TaskEndedAction += DisableScreen;
        }

        /// <summary>
        /// Enables the screen by enableing the Canvas component
        /// </summary>
        /// <param name="state"></param>
        void EnableScreen(bool state)
        {
            if (state != ActivateOnWinState)
                return;

            canvasComponent.enabled = true;
        }

        /// <summary>
        /// Disables the screen by disableing the Canvas component
        /// </summary>
        /// <param name="state"></param>
        void DisableScreen(bool state)
        {
            if (state == ActivateOnWinState)
                return;

            canvasComponent.enabled = false;
        }
    }
}