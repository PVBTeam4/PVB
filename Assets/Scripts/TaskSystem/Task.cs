using System;
using System.Collections.Generic;
using Global;
using UnityEngine;

namespace TaskSystem
{
    /// <summary>
    /// Class that will keep track of all Objectives within Task
    /// </summary>
    public abstract class Task
    {
        private float _secondsActive;
        
        public float SecondsActive => _secondsActive;

        // ToolType used to complete task
        private readonly ToolType _toolType;
        
        // Action that will let TaskController know, Task is completed
        private readonly Action<Task> _taskCompleteAction;
        
        // Array of all active Objectives (not yet completed)
        protected readonly List<Objective> ActiveObjectives = new List<Objective>();

        /// <summary>
        /// Constructor of Task
        /// </summary>
        /// <param name="toolType">ToolType used to complete Task</param>
        /// <param name="taskCompleteAction">Action for Task completion</param>
        public Task(ToolType toolType, Action<Task> taskCompleteAction)
        {
            _toolType = toolType;
            _taskCompleteAction = taskCompleteAction;
        }

        public void Update()
        {
            _secondsActive += Time.deltaTime;
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
            ActiveObjectives.Remove(completedObjective);

            if (isTaskCompleted())
            {
                OnTaskCompletion();
            }
        }

        protected abstract bool isTaskCompleted();

        /// <summary>
        /// Call TaskComplete action, to let TaskController know
        /// Task is completed.
        /// </summary>
        private void OnTaskCompletion()
        {
            _taskCompleteAction?.Invoke(this);
        }

        /// <summary>
        /// Returns this Task's tooltype
        /// </summary>
        /// <returns></returns>
        public ToolType GetToolType()
        {
            return _toolType;
        }

        public void RegisterObjective(Objective objective)
        {
            ActiveObjectives.Add(objective);
            objective.InitializeObjective(OnObjectiveCompletion);
        }
    }
}
