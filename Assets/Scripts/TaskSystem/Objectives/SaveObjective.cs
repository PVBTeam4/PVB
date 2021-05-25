using Boat;
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

        //This 'boatManager' is basically whatever object takes care of the boats
        //It is needed in this script to notify the game that a refugeeship has died

        private GameObject _boatsKilledUIObject;
        private GameObject _boatsSavedUIObject;

        [SerializeField, TagSelector]
        private string shipsKilledUIObject;

        [SerializeField, TagSelector]
        private string shipsSavedUIObject;

        //[SerializeField] private UnityEvent<int> onDeath;
        public UnityAction<int, int> onDeath;

        public UnityAction<int, int> onTargetReached;

        private void Start()
        {
            _boatsKilledUIObject = GameObject.FindGameObjectWithTag(shipsKilledUIObject);
            onDeath += _boatsKilledUIObject.GetComponent<UpdateUICounter>().updateCounter;

            _boatsSavedUIObject = GameObject.FindGameObjectWithTag(shipsSavedUIObject);
            onTargetReached += _boatsSavedUIObject.GetComponent<UpdateUICounter>().updateCounter;
        }


        /// <summary>
        /// The objective is completed once the object reaches the end target
        /// Unsubscribes OnTaskEnd so that the object won't be destroyed twice
        /// </summary>
        public void ReachEndTarget()
        {
            CompleteObjective();

            onTargetReached(Values.TaskValues.RefugeeShipsSaved, Values.TaskValues.RefugeeShipsToSave);

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
            }

            onDeath(Values.TaskValues.RefugeeShipsKilled, Values.TaskValues.RefugeeShipsToSave);

            //boatManager.GetComponent<BoatSpawner>().onRefugeeShipDeath.Invoke();
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
