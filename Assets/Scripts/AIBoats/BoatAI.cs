using System;
using Global;
using Properties.Tags;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace AIBoats
{
    /// <summary>
    /// this class causes the assigned boat object to move towards the target
    ///  before it moves it rotate to the target
    /// it set the boat as the same y position of the target
    /// </summary>
    public class BoatAI : MonoBehaviour
    {
        [SerializeField, TagSelector]
        // The tag of the target. To get the Transform from
        private string targetTag = "Player";

        // Transform component of the target
        private Transform _targetTransform;

        // Speed values
        [Header("Speed Values"), SerializeField]
        private float minSpeed, maxSpeed;
        // Percentage: 0 - 1
        [SerializeField, Range(0, .1f)]
        private float percentageIncreasePerSecond;

        // NavMeshAgent component of this object
        private NavMeshAgent _navMeshAgent;

        // This is run befor the Start function
        private void Awake()
        {
            CheckAndGetComponents();
            _targetTransform = GameObject.FindWithTag(targetTag).transform;

            // Look at target on spawn
            transform.LookAt(_targetTransform.position);
        }

        /// <summary>
        /// Checks if the needed components exists. 
        /// If so set set the targetTransform & call StartMovementTowardsTarget. 
        /// If not log error
        /// </summary>
        private void CheckAndGetComponents()
        {
            // Check and Get the NavMeshAgent
            _navMeshAgent = GetComponent<NavMeshAgent>();

            if (_navMeshAgent == null)
            {
                Debug.LogError("NavMeshAgent component not attached" + gameObject.name);
                return;
            }

            _navMeshAgent.speed = CalculateSpeed();

            // Find the gameobject with the targetTag
            GameObject _targetObject = GameObject.FindWithTag(targetTag);

            // Check if the destinationObject exists, if not log error
            if (_targetObject == null)
            {
                Debug.LogError("Target object: " + targetTag + ", has not been found");
                return;
            }

            // Set the target transform to that of the target transform component
            _targetTransform = _targetObject.transform;

            // Start movement
            StartMovementTowardsTarget();
        }

        /// <summary>
        /// Start moving towards the target via the NavMeshAgent component
        /// </summary>
        private void StartMovementTowardsTarget()
        {
            // Make sure the Y-Axis does not get affected
            Vector3 _targetPosition = _targetTransform.position;
            _targetPosition.y = transform.position.y;

            // Move via the NavMeshAgent towards the targetPosition
            _navMeshAgent.SetDestination(_targetPosition);

            // Create a "FixedUpdate" like loop for the "UpdateNavMeshDestination" function
            InvokeRepeating("UpdateNavMeshDestination", 0f, Time.fixedDeltaTime);
        }

        /// <summary>
        /// This will make sure the Y-Axis does not get affected
        /// </summary>
        private void UpdateNavMeshDestination()
        {
            // Make sure the Y-Axis does not get affected
            Vector3 correctPos = _navMeshAgent.nextPosition;
            correctPos.y = transform.position.y;

            // Update the position of this object to that of the 
            transform.position = correctPos;
        }

        private float CalculateSpeed()
        {
            float secondsActive = GameManager.Instance.TaskController.ActiveTask.SecondsActive;
            float percentage = Math.Min(secondsActive * percentageIncreasePerSecond, 1);

            float min = minSpeed;
            float max = Mathf.Lerp(minSpeed, maxSpeed, percentage);

            return Random.Range(min, max);
        }
    }
}
