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
    private float damage;

    [SerializeField]
    // Transform of the destination
    private Transform _destination;

    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        // Get the components used for the navigation
        _navMeshAgent = GetComponent<NavMeshAgent>();

        // Start the navigation logic
        SetDestination();
    }

    /// <summary>
    /// Starts the navigation logic. Will move towards the target the moment its called
    /// </summary>
    private void SetDestination()
    {
        // Check if the navAgent exists, if not log error
        if (_navMeshAgent == null)
        {
            Debug.LogError("navAgent component not attached" + gameObject.name);
        }
        else// Run the function to move towards the destination
        {
            BeginMovingTowardsDestination(_destination);
        }
    }

    /// <summary>
    /// Will move this object towards the given destination using the NavMeshAgent
    /// </summary>
    /// <param name="destinationVector">Transform Of the target the object needs to move towards</param>
    private void BeginMovingTowardsDestination(Transform _destinationTransform)
    {
        // Check if the destination exists, log error if not
        if (_destinationTransform == null)
        {
            Debug.LogError("Destination Transform does not exist");
        }
        else// Run the destination logic
        {
            // Move via the NavMeshAgent towards the destinationVector
            _navMeshAgent.SetDestination(_destinationTransform.position);
        }
    }
}
