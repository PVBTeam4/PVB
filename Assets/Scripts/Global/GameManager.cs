using UnityEngine;
using System;

namespace Global
{
    /// <summary>
    /// Center point of our game.
    /// This Manager will keep track of all other Managers.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        // Private variable to hold this instance of the GameManager Class
        private GameManager _instance;

        // Returns this instance of the GameManager Class
        public GameManager Instance { get { return _instance; } }

        // Fire this event when you want to switch to the Over World Scene
        public static Action SwitchedSceneToOverworld;

        // Fire this event when you want to switch to a Tool Scene
        public static Action<ToolType> SwitchedSceneByToolType;

        /// <summary>
        /// On GameObject enable
        /// </summary>
        private void OnEnable()
        {
            // Set the instance to this GameManager Class
            _instance = this;
        }

        /// <summary>
        /// Switches to the right Scene via the ToolType enum
        /// </summary>
        /// <param name="toolType">Type of tool</param>
        public void EnterTaskMode(ToolType toolType)
        {
            // Fire the Action Event
            SwitchedSceneByToolType.Invoke(toolType);
        }

        /// <summary>
        /// Switches back to the OverWorld Scene
        /// </summary>
        private void EnterOverWorld()
        {
            // Fire the Action Event
            SwitchedSceneToOverworld.Invoke();
        }
    }
}
