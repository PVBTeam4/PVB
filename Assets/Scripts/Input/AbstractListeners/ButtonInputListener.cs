using UnityEngine;

namespace Input.AbstractListeners
{
    /// <summary>
    /// Abstract class that contains receive methods for input.
    /// Will register & de-register the receive input methods.
    /// </summary>
    public abstract class ButtonInputListener
    {
        /// <summary>
        /// Will register the receive input methods
        /// </summary>
        protected void RegisterInput()
        {
            InputManager.ButtonInputAction += OnInputReceived;
            InputManager.MouseMovementAction += OnMouseMovementReceived;
        }

        /// <summary>
        /// Will de-register the receive input methods
        /// </summary>
        protected void DeRegisterInput()
        {
            InputManager.ButtonInputAction -= OnInputReceived;
            InputManager.MouseMovementAction -= OnMouseMovementReceived;
        }

        /// <summary>
        /// Will be activated when a "button" input is received
        /// </summary>
        /// <param name="buttonInputType">Type of input received</param>
        /// <param name="value">Value of input received</param>
        protected abstract void OnInputReceived(ButtonInputType buttonInputType, float value);

        /// <summary>
        /// Will be activated when the mouse movement input is received
        /// </summary>
        /// <param name="mousePosition">Position of mouse on screen pixels</param>
        protected abstract void OnMouseMovementReceived(Vector3 mousePosition);
    }
}