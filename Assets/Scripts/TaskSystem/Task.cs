using System;
using System.Linq;
using Global;
using UnityEngine;

namespace TaskSystem
{
    /// <summary>
    /// Class that will keep track of all Objectives within Task
    /// </summary>
    public class Task
    {
        // ToolType used to complete task
        private readonly ToolType _toolType;
        
        // Action that will let TaskController know, Task is completed
        private readonly Action<Task> _taskCompleteAction;
        
        // Array of all active Objectives (not yet completed)
        private Objective[] _activeObjectives;

        /// <summary>
        /// Constructor of Task
        /// </summary>
        /// <param name="toolType">ToolType used to complete Task</param>
        /// <param name="taskCompleteAction">Action for Task completion</param>
        /// <param name="activeObjectives">Array of active Objectives</param>
        public Task(ToolType toolType, Action<Task> taskCompleteAction, Objective[] activeObjectives)
        {
            _toolType = toolType;
            _taskCompleteAction = taskCompleteAction;
            _activeObjectives = activeObjectives;

            InitializeObjectives();
        }

        /// <summary>
        /// Will be called when a active Objective is completed.
        /// Responsible for:
        /// Updating active Objectives;
        /// Completing Task;
        /// </summary>
        /// <param name="completedObjective">Objective that is completed</param>
        public void OnObjectiveCompletion(Objective completedObjective)
        {
            // Remove objective from array
            _activeObjectives = _activeObjectives.Where(objective => !completedObjective.Equals(objective)).ToArray();

            Debug.Log("Objective complete");
            
            if (_activeObjectives.Length == 0)
            {
                OnTaskCompletion();
            }
        }

        /// <summary>
        /// Call TaskComplete action, to let TaskController know
        /// Task is completed.
        /// </summary>
        private void OnTaskCompletion()
        {
            _taskCompleteAction?.Invoke(this);
        }

        /// <summary>
        /// Initializes all active objectives.
        /// NEEDS TO BE CALLED ON TASK CREATION!
        /// </summary>
        private void InitializeObjectives()
        {
            foreach (Objective activeObjective in _activeObjectives)
            {
                activeObjective.InitializeObjective(OnObjectiveCompletion);
            }
        }
    }
}
