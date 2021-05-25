﻿using System;
using Global;
using Values;

namespace TaskSystem.Tasks
{
    public class CannonTask : Task
    {
        private int _refugeeShipsToSave;

        public CannonTask(ToolType toolType, Action<Task> taskCompleteAction) : base(toolType, taskCompleteAction)
        {
            _refugeeShipsToSave = TaskValues.RefugeeShipsToSave;
        }

        protected override bool isTaskCompleted()
        {
            _refugeeShipsToSave--;

            // Clamp to zero
            _refugeeShipsToSave = Math.Max(_refugeeShipsToSave, 0);

            return _refugeeShipsToSave == 0;
        }
    }
}