using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour 
{
    [SerializeField]
    private float followSpeed = 7;

    [SerializeField]
    private Transform targetTransform;

    void FixedUpdate()
	{
        // Follow the target by lerping towards it
        LerpToTarget(targetTransform, followSpeed);
    }

    void LerpToTarget(Transform _targetTransform, float _followSpeed)
    {
        // Check if the targetTransform has been filled in
        if (targetTransform)
        {
            transform.position = Vector3.Lerp(transform.position, _targetTransform.position, _followSpeed * Time.deltaTime);
        }
        else
        {
            // Else if it does not exits, log an Error
            Debug.LogError("Camera target to follow not found");
        }
    }
}
