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

        /// <summary>
        /// The objective is completed once the object reaches the end target
        /// Unsubscribes OnTaskEnd so that the object won't be destroyed twice
        /// </summary>
        public void ReachEndTarget()
        {
            CompleteObjective();
            Destroy(gameObject);
        }

        /// <summary>
        /// If a refugeeship is killed by the player, its death will be counted. If the player has killed as many refugeeships as he is supposed to save, he loses
        /// the game.
        /// </summary>
        public void OnDeath()
        {
            Values.TaskValues.RefugeeShipsKilled++;
            if(Values.TaskValues.RefugeeShipsKilled >= Values.TaskValues.RefugeeShipsToSave)
            {
                gameObject.GetComponent<TaskFail>().FailTask();
                Values.TaskValues.RefugeeShipsKilled = 0;
            }
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
