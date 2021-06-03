using Global;
using UnityEngine;
using System;
using TaskSystem.Tasks;

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

        public Task ActiveTask
        {
            get
            {
                return _activeTask;
            }
        }

        // Action event that will be run when the Task is completed or failed
        // Bool: 'True' if completed, 'False' if failed
        public static event Action<ToolType, bool> TaskEndedAction;

        public TaskController(ToolType toolType)
        {
            _activeTask = CreateTaskForTool(toolType);
        }
        
        
        public void Update()
        {
            _activeTask?.Update();
        }

        /// <summary>
        /// Cancel the current Task.
        /// Will send FAIL event
        /// </summary>
        public void CancelActiveTask()
        {
            HandleTaskResult(false);
            
            // Clear TaskEndedActions
            TaskEndedAction = null;
        }

        /// <summary>
        /// Create Task for specific Tool
        /// </summary>
        /// <param name="toolType">ToolType to create Task</param>
        /// <returns></returns>
        private Task CreateTaskForTool(ToolType toolType)
        {
            switch (toolType)
            {
                case ToolType.CANNON:
                    return new CannonTask(toolType, OnTaskCompletion);
            }
            return null;
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
            if (_activeTask == null) return;
            
            Task completedTask = _activeTask;
            
            // Reset active task
            _activeTask = null;

            // Call the event that the player completed the Task
            TaskEndedAction?.Invoke(completedTask.GetToolType(), isTaskCompleted);
        }
    }
}
