using System.Collections.Generic;
using Global;
using TaskSystem.Objectives;
using UnityEngine;
using Utils;
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

        // All tasks, by ToolType
        private Dictionary<ToolType, Task> _tasks;

        /// <summary>
        /// Initialize tasks for every ToolType &
        /// add these tasks to the Dictionary.
        /// </summary>
        public void InitializeTasksForEveryTool()
        {
            var dictionary = new Dictionary<ToolType, Task>();
            foreach (var toolType in EnumUtil.GetValues<ToolType>())
            {
                dictionary[toolType] = CreateTaskForTool(toolType);
            }
            _tasks = dictionary;
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
        /// Activate Task for a specific tool
        /// </summary>
        /// <param name="toolType">ToolType to activate Task</param>
        public void ActivateTaskForTool(ToolType toolType)
        {
            if (_tasks == null || _tasks.Count == 0)
            {
                Debug.LogError("There are no tasks registered.");
                return;
            }
            
            if (_tasks.TryGetValue(toolType, out var task))
            {
                _activeTask = task;
            }
            else
            {
                Debug.LogError("Couldn't find any task for tool: " + toolType);
            }
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
            var objectives = GetObjectivesInSceneByToolType(toolType);
            var task = new Task(OnTaskCompletion, objectives);
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
            
            Debug.Log("Congrats, you completed the task!");
        }
    }
}
