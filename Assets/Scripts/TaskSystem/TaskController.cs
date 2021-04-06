using Global;
using TaskSystem.Objectives;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TaskSystem
{
    /// <summary>
    /// Class responsible for:
    /// Task creation, completion, cancellation
    /// </summary>
    public class TaskController
    {
        // Current active task
        private Task _activeTask;

        public TaskController(ToolType toolType)
        {
            _activeTask = CreateTaskForTool(toolType);
        }

        /// <summary>
        /// Cancel the current Task
        /// </summary>
        public void CancelActiveTask()
        {
            if (_activeTask == null)
            {
                Debug.LogError("There is no active task, to cancel.");
                return;
            }

            _activeTask = null;
            Debug.Log("Active Task has been cancelled.");
        }

        /// <summary>
        /// Get all Objectives in scene, by their ToolType
        /// </summary>
        /// <param name="toolType">ToolType to specify Objective</param>
        /// <returns></returns>
        private Objective[] GetObjectivesInSceneByToolType(ToolType toolType)
        {
            switch (toolType)
            {
                case ToolType.CANNON:
                    return Object.FindObjectsOfType<KillObjective>();
            }
            return null;
        }

        /// <summary>
        /// Create Task for specific Tool
        /// </summary>
        /// <param name="toolType">ToolType to create Task</param>
        /// <returns></returns>
        private Task CreateTaskForTool(ToolType toolType)
        {
            Objective[] objectives = GetObjectivesInSceneByToolType(toolType);
            Task task = new Task(toolType, OnTaskCompletion, objectives);
            return task;
        }
        
        /// <summary>
        /// Will be called when Task is completed
        /// </summary>
        /// <param name="completedTask">Task that is completed</param>
        private void OnTaskCompletion(Task completedTask)
        {
            Debug.Log("Task completed");
            if (!completedTask.Equals(_activeTask))
            {
                Debug.LogError("Other task got completed, instead of the active one!");
                return;
            }
            
            // Switch back to overworld
            SceneController.SwitchSceneToOverWorld();
            Debug.Log("Congrats, you completed the task!");
        }
    }
}
