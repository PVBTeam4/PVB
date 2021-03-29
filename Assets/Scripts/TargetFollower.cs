using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour 
{
    [SerializeField]
    private float followSpeed;

    [SerializeField]
    private Transform targetTransform;

    void FixedUpdate()
	{
        transform.position = Vector3.Lerp(transform.position, targetTransform.position, followSpeed * Time.deltaTime);
    }
}
