using System.Collections.Generic;
using Global;
using TaskSystem;

namespace Statistics
{
    public class StatisticsController
    {
        public bool CompletedCannonTask = false;

        private readonly List<ToolType> _completedTasks = new List<ToolType>();

        public void ConstructController()
        {
            TaskController.TaskEndedAction += OnTaskCompleted;
        }

        public void DeconstructController()
        {
            TaskController.TaskEndedAction -= OnTaskCompleted;
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