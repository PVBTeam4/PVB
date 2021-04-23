using System;
using System.Linq;
using Global;
using TaskSystem.Objectives;
using UnityEngine;
using Object = UnityEngine.Object;

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
        public Objective[] ActiveObjectives { get; private set; }

        /// <summary>
        /// Constructor of Task
        /// </summary>
        /// <param name="toolType">ToolType used to complete Task</param>
        /// <param name="taskCompleteAction">Action for Task completion</param>
        public Task(ToolType toolType, Action<Task> taskCompleteAction)
        {
            _toolType = toolType;
            _taskCompleteAction = taskCompleteAction;
            ActiveObjectives = GetObjectivesInSceneByToolType(toolType);

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
            ActiveObjectives = ActiveObjectives.Where(objective => !completedObjective.Equals(objective)).ToArray();

            Debug.Log("Objective complete");
            
            if (ActiveObjectives.Length == 0)
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
            foreach (Objective activeObjective in ActiveObjectives)
            {
                activeObjective.InitializeObjective(OnObjectiveCompletion);
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
    }
}
