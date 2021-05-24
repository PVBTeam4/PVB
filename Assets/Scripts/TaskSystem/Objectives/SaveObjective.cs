using Global;
using Gun;
using Properties.Tags;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace TaskSystem.Objectives
{
    /// <summary>
    /// Component that will both update the health when damaged by a bullet and notify the tasksystem 
    /// when it has reached a particular target
    /// </summary>
    public class SaveObjective : Objective
    {
        //Tag for the target which will mean the ojective's completion
        [SerializeField, TagSelector]
        private string targetTag;

        private void Start()
        {
            TaskController.TaskEndedAction += OnTaskEnd;
        }

        /// <summary>
        /// The objective is completed once the object reaches the end target
        /// Unsubscribes OnTaskEnd so that the object won't be destroyed twice
        /// </summary>
        public void ReachEndTarget()
        {
            CompleteObjective();
            TaskController.TaskEndedAction -= OnTaskEnd;

            Destroy(gameObject);
        }

        /// <summary>
        /// Cleans up refugee ships that may still be active after the task has already ended
        /// </summary>
        /// <param name="toolType"></param>
        /// <param name="isCompleted"></param>
        private void OnTaskEnd(ToolType toolType, bool isCompleted)
        {
            if (toolType != ToolType.CANNON) return;
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(targetTag))
            {
                ReachEndTarget();
            }
        }
    }
}
