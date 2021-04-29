using Global;
using UnityEngine;
using System;

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

        // Action event that will be run when the Task is completed or failed
        public static event Action<bool> TaskEndedAction;// True if completed, False if failed

        public TaskController(ToolType toolType)
        {
            _activeTask = CreateTaskForTool(toolType);
        }

        /// <summary>
        /// Cancel the current Task.
        /// Will send FAIL event
        /// </summary>
        public void CancelActiveTask()
        {
            HandleTaskResult(false);
        }

        /// <summary>
        /// Create Task for specific Tool
        /// </summary>
        /// <param name="toolType">ToolType to create Task</param>
        /// <returns></returns>
        private Task CreateTaskForTool(ToolType toolType)
        {
            Task task = new Task(toolType, OnTaskCompletion);
            return task;
        }
        
        /// <summary>
        /// Will be called when Task is completed
        /// </summary>
        /// <param name="completedTask">Task that is completed</param>
        private void OnTaskCompletion(Task completedTask)
        {
            if (!completedTask.Equals(_activeTask))
            {
                Debug.LogError("Other task got completed, instead of the active one!");
                return;
            }
            
            HandleTaskResult(true);
        }

        private void HandleTaskResult(bool isTaskCompleted)
        {
            // Reset active task
            _activeTask = null;

            // Call the event that the player completed the Task
            TaskEndedAction?.Invoke(isTaskCompleted);

            // Switch back to overWorld
            SceneController.SwitchSceneToOverWorld();
        }
    }
}
