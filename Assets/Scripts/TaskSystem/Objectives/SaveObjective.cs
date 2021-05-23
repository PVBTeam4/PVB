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

        /// <summary>
        /// The objective is completed once the object reaches the end target
        /// </summary>
        public void ReachEndTarget()
        {
            CompleteObjective();
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
