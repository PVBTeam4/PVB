using System.Collections.Generic;
using Global;
using TaskSystem;

namespace Statistics
{
    public class StatisticsTracker
    {
        public bool CompletedCannonTask = false;

        private readonly List<ToolType> _completedTasks = new List<ToolType>();

        public StatisticsTracker()
        {
            TaskController.TaskEndedAction += OnTaskCompleted;
        }

        private void OnTaskCompleted(ToolType toolType, bool isTaskCompleted)
        {
            // Return is task is not completed
            if (!isTaskCompleted) return;

            _completedTasks.Add(toolType);
        }

        public bool isTaskCompleted(ToolType toolType)
        {
            return _completedTasks.Contains(toolType);
        }
    }
}