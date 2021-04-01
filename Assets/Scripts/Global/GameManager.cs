using UnityEngine;
using TaskSystem;

namespace Global
{
    /// <summary>
    /// Center point of our game.
    /// This Manager will keep track of all other Managers.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
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

        private void OnEnterTaskMode(ToolType toolType)
        {
            ToolController = new ToolController(toolType);
            TaskController = new TaskController(toolType);
        }
        
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
