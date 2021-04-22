using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TaskSystem
{
    public class ScreenActivator : MonoBehaviour
    {
        private Action<bool> screenActivationAction;

        void Awake()
        {
            TaskController controller = GameObject.FindObjectOfType(typeof(TaskController));

            if (controller)
            {
                
            }
        }

        void EnableScreen()
        {
            print("Enable SCREEN");
        }

        void DisableScreen()
        {
            print("Disable SCREEN");
        }
    }
}