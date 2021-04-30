using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This class causes the enemy to move towards the target using the NavMeshAgent
/// </summary>
public class EnemyAI_old : MonoBehaviour
{
    [SerializeField]
    // speed of Enemy 
    private float speed = 2f;
    [SerializeField]
    // damage when collide target
    private float damage;

    // Destination Variables

    // Transform of the destination
    private Transform destinationTransform;

    [SerializeField]
    // The tag of the target. To get the Transform from
    private string targetTag = "Player";

    private Vector3 _destinationPosition;

    // Navigation Variables

    private NavMeshAgent _navMeshAgent;

    // This is run befor the Start function
    private void Awake()
    {
        SetDestination();
    }

    // This is run once
    private void Start()
    {
        // Get the components used for the navigation
        _navMeshAgent = GetComponent<NavMeshAgent>();

        // Start the navigation logic
        StartDestination();
    }

    /// <summary>
    /// Sets the destination Transform by finding the Transform of the target object via the targetTag
    /// </summary>
    private void SetDestination()
    {
        // Find the gameobject with the targetTag
        GameObject _destinationObject = GameObject.FindWithTag(targetTag);

        // Check if the destinationObject exists, if not log error
        if (_destinationObject == null)
        {
            Debug.LogError("Destination object has not been found");
        }
        else// Set the destination
        {
            destinationTransform = _destinationObject.transform;
        }
    }

    /// <summary>
    /// Starts the navigation logic. Will move towards the target the moment its called
    /// </summary>
    private void StartDestination()
    {
        // Check if the navAgent exists, if not log error
        if (_navMeshAgent == null)
        {
            Debug.LogError("navAgent component not attached" + gameObject.name);
        }
        else// Run the function to move towards the destination
        {
            BeginMovingTowardsDestination(destinationTransform);
        }
    }

    /// <summary>
    /// Will move this object towards the given destination using the NavMeshAgent
    /// The Y axis will not be affected
    /// </summary>
    /// <param name="destinationTransform">Transform Of the target the object needs to move towards</param>
    private void BeginMovingTowardsDestination(Transform _destinationTransform)
    {
        // Check if the destination exists, log error if not
        if (_destinationTransform == null)
        {
            Debug.LogError("Destination Transform does not exist");
        }
        else// Run the destination logic
        {
            // Make sure the Y-Axis does not get affected
            Vector3 _destinationPosition = _destinationTransform.position;
            _destinationPosition.y = transform.position.y;

            // Move via the NavMeshAgent towards the destinationVector
            _navMeshAgent.SetDestination(_destinationPosition);
        }
    }

    void FixedUpdate()
    {
        // 
        UpdateNavMeshDestination();
    }

    /// <summary>
    /// This will make sure the Y-Axis does not get affected
    /// </summary>
    private void UpdateNavMeshDestination()
    {
        // Make sure the Y-Axis does not get affected
        Vector3 nextPos = _navMeshAgent.nextPosition;
        Vector3 correctPos = nextPos;
        correctPos.y = transform.position.y;

        // Update the position of this object to that of the 
        transform.position = correctPos;
    }
}
