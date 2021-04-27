using System;
using UnityEngine;

namespace TaskSystem
{
    /// <summary>
    /// Objective base class
    /// </summary>
    public abstract class Objective : MonoBehaviour
    {
        // Action that will let Task know, Objective is completed
        private Action<Objective, bool> _completeObjectiveAction;

        /// <summary>
        /// Initializes Objective
        /// FIRST THING TO BE CALLED BY TASK
        /// </summary>
        public void InitializeObjective(Action<Objective, bool> completeObjectiveAction)
        {
            _completeObjectiveAction = completeObjectiveAction;
        }

        /// <summary>
        /// Complete Objective
        /// </summary>
        protected void CompleteObjective(bool won)
        {
            if (_completeObjectiveAction == null)
            {
                Debug.LogError("Objective has not been initialized!");
                return;
            }
            
            _completeObjectiveAction.Invoke(this, won);
        }
    }
}
