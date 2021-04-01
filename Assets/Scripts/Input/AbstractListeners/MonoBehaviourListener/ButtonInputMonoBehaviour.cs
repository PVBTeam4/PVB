using UnityEngine;

namespace Input.AbstractListeners.MonoBehaviourListener
{
    /// <summary>
    /// Abstract class that contains receive methods for input.
    /// Will register & de-register the receive input methods.
    /// </summary>
    public abstract class ButtonInputMonoBehaviour : MonoBehaviour
    {
        /// <summary>
        /// Will register the receive input methods
        /// </summary>
        private void OnEnable()
        {
            InputManager.ButtonInputAction += OnInputReceived;
        }

        /// <summary>
        /// Will de-register the receive input methods
        /// </summary>
        private void OnDisable()
        {
            InputManager.ButtonInputAction -= OnInputReceived;
        }

        /// <summary>
        /// Will be activated when a "button" input is received
        /// </summary>
        /// <param name="buttonInputType">Type of input received</param>
        /// <param name="value">Value of input received</param>
        protected abstract void OnInputReceived(ButtonInputType buttonInputType, float value);
    }
}
