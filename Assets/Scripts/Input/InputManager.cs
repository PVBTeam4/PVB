using System;
using UnityEngine;

namespace Input
{
    /// <summary>
    /// Manages player input
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        // Horizontal input axis name
        [SerializeField] private string horizontalInputName;
        // Vertical input axis name
        [SerializeField] private string verticalInputName;
    
        // Button input action
        public static Action<ButtonInputType, float> ButtonInputAction;
        // Mouse movement input action
        public static Action<Vector3> MouseMovementAction;

        /// <summary>
        /// Will handle the button & mouse movement input
        /// </summary>
        private void Update()
        {
            HandleAxisInput();
            HandleMouseInput();
        }

        /// <summary>
        /// Handles horizontal & vertical axis input
        /// </summary>
        private void HandleAxisInput()
        {
            ButtonInputAction?.Invoke(ButtonInputType.HorizontalMovement, UnityEngine.Input.GetAxisRaw(horizontalInputName));
            ButtonInputAction?.Invoke(ButtonInputType.VerticalMovement, UnityEngine.Input.GetAxisRaw(verticalInputName));
        }

        /// <summary>
        /// Handles button button & mouse movement input
        /// </summary>
        private void HandleMouseInput()
        {
            // Button input
            ButtonInputAction?.Invoke(ButtonInputType.LeftMouse, UnityEngine.Input.GetMouseButtonDown(0) ? 1f : 0f);
            ButtonInputAction?.Invoke(ButtonInputType.RightMouse, UnityEngine.Input.GetMouseButton(1) ? 1f : 0f);
            
            // Mouse movement
            MouseMovementAction?.Invoke(UnityEngine.Input.mousePosition);
        }
    }
}
