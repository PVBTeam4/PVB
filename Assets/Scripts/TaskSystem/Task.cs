using System;
using System.Linq;

namespace TaskSystem
{
    /// <summary>
    /// Class that will keep track of all Objectives within Task
    /// </summary>
    public class Task
    {
        // Action that will let TaskController know, Task is completed
        private readonly Action<Task> _taskCompleteAction;
        
        // Array of all active Objectives (not yet completed)
        private Objective[] _activeObjectives;

        /// <summary>
        /// Constructor of Task
        /// </summary>
        /// <param name="taskCompleteAction">Action for Task completion</param>
        /// <param name="activeObjectives">Array of active Objectives</param>
        public Task(Action<Task> taskCompleteAction, Objective[] activeObjectives)
        {
            _taskCompleteAction = taskCompleteAction;
            _activeObjectives = activeObjectives;
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
    }
}
