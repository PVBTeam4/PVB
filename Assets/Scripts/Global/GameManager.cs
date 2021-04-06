using UnityEngine;
using TaskSystem;
using ToolSystem;

namespace Global
{
    /// <summary>
    /// Center point of our game.
    /// This Manager will keep track of all other Managers.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        // These
        public ToolController ToolController;
        public TaskController TaskController;

        // Private variable to hold this instance of the GameManager Class
        private GameManager _instance;

        // Returns this instance of the GameManager Class
        public GameManager Instance { get { return _instance; } }

        /// <summary>
        /// On GameObject enable
        /// </summary>
        private void OnEnable()
        {
            // Set the instance to this GameManager Class
            _instance = this;

            SceneController.OverWorldEnterAction += OnEnterOverWorld;
            SceneController.TaskModeEnterAction += OnEnterTaskMode;
        }

        /// <summary>
        /// When the player enters a task trigger. Run this method it will assign the controllers
        /// This method will be run when the TaskModeEnterAction event in fired
        /// </summary>
        /// <param name="toolType"></param>
        private void OnEnterTaskMode(ToolType toolType)
        {
            ToolController = new ToolController(toolType);
            TaskController = new TaskController(toolType);
        }

        /// <summary>
        /// When the player is done with a task. Run this method, it will deactivate the controllers
        /// The method will be run when the OverWorldEnterAction event in fired
        /// </summary>
        private void OnEnterOverWorld()
        {
            // Deactivate controllers
            if (ToolController != null)
            {
                ToolController.DeConstruct();
                ToolController = null;
            }

            if (TaskController != null)
            {
                TaskController.CancelActiveTask();
                TaskController = null;
            }
        }
    }
}
