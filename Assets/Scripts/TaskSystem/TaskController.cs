using Global;
using UnityEngine;

namespace TaskSystem
{
    /// <summary>
    /// Class responsible for:
    /// Task creation, completion, cancellation
    /// </summary>
    public class TaskController
    {
        // Current active task
        public readonly Task ActiveTask;

        public TaskController(ToolType toolType)
        {
            ActiveTask = CreateTaskForTool(toolType);
        }

        /// <summary>
        /// Cancel the current Task
        /// </summary>
        public void CancelActiveTask()
        {
            if (ActiveTask == null)
            {
                Debug.LogError("There is no active task, to cancel.");
                return;
            }
            
            Debug.Log("Active Task has been cancelled.");
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
            Debug.Log("Task completed");
            if (!completedTask.Equals(ActiveTask))
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
