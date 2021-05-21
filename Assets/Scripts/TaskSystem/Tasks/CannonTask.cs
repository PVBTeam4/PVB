using System;
using Global;
using Values;

namespace TaskSystem.Tasks
{
    public class CannonTask : Task
    {
        private int _enemiesToKill;

        public CannonTask(ToolType toolType, Action<Task> taskCompleteAction) : base(toolType, taskCompleteAction)
        {
            _enemiesToKill = TaskValues.EnemiesToKill;
        }

        protected override bool isTaskCompleted()
        {
            _enemiesToKill--;

            // Clamp to zero
            _enemiesToKill = Math.Max(_enemiesToKill, 0);

            return _enemiesToKill == 0;
        }
    }
}