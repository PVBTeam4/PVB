using Global;
using SceneSystem;
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
        [SerializeField]
        public static event Action<bool> TaskEndedAction;// True if completed, False if failed

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

            // Call the event that the player lost the Task
            //TaskEndedAction?.Invoke(false);

            _activeTask = null;
            //Debug.Log("Active Task has been cancelled.");
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
        private void OnTaskCompletion(Task completedTask, bool won)
        {
            //Debug.Log("Task completed");
            if (!completedTask.Equals(_activeTask))
            {
                Debug.LogError("Other task got completed, instead of the active one!");
                return;
            }

            // Call the event that the player completed the Task
            TaskEndedAction?.Invoke(won);

            // Switch back to overworld
            SceneController.SwitchSceneToOverWorld();
            //Debug.Log("Congrats, you completed the task!");
        }
    }
}
