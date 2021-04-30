using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This class causes the enemy to move towards the target using the NavMeshAgent
/// </summary>
public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    // speed of Enemy 
    private float speed = 2f;

    [SerializeField]
    // damage when collide target
    private float damage = 1;

    [SerializeField]
    // The tag of the target. To get the Transform from
    private string targetTag = "Player";

    // Transform component of the target
    private Transform targetTransform;

    // NavMeshAgent component of this object
    private NavMeshAgent navMeshAgent;

    // This is run befor the Start function
    private void Awake()
    {
        CheckAndGetComponents();
    }

    /// <summary>
    /// Checks if the needed components exists. 
    /// If so set set the targetTransform & call StartMovementTowardsTarget. 
    /// If not log error
    /// </summary>
    private void CheckAndGetComponents()
    {
        // Check and Get the NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not attached" + gameObject.name);
            return;
        }

        // Find the gameobject with the targetTag
        GameObject _targetObject = GameObject.FindWithTag(targetTag);

        // Check if the destinationObject exists, if not log error
        if (_targetObject == null)
        {
            Debug.LogError("Target object: " + targetTag + ", has not been found");
            return;
        }

        // Set the target transform to that of the target transform component
        targetTransform = _targetObject.transform;

        // Start movement
        StartMovementTowardsTarget();
    }

    /// <summary>
    /// Start moving towards the target via the NavMeshAgent component
    /// </summary>
    private void StartMovementTowardsTarget()
    {
        // Make sure the Y-Axis does not get affected
        Vector3 _targetPosition = targetTransform.position;
        _targetPosition.y = transform.position.y;

        // Move via the NavMeshAgent towards the targetPosition
        navMeshAgent.SetDestination(_targetPosition);

        // Create a "FixedUpdate" like loop for the "UpdateNavMeshDestination" function
        InvokeRepeating("UpdateNavMeshDestination", 0f, Time.fixedDeltaTime);
    }

    /// <summary>
    /// This will make sure the Y-Axis does not get affected
    /// </summary>
    private void UpdateNavMeshDestination()
    {
        // Make sure the Y-Axis does not get affected
        Vector3 nextPos = navMeshAgent.nextPosition;
        Vector3 correctPos = nextPos;
        correctPos.y = transform.position.y;

        // Update the position of this object to that of the 
        transform.position = correctPos;
    }
}
