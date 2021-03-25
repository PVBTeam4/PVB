using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsTransform : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private float rotationSpeed = 1f;

    [SerializeField]
    private Vector3 rotationSpeedScale = new Vector3(1,1,1);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTransform)
        {
            Quaternion _rotation = GetRotationTowardsTransform(this.transform, targetTransform);
            _rotation.x *= rotationSpeedScale.x;
            _rotation.y *= rotationSpeedScale.y;
            _rotation.z *= rotationSpeedScale.z;

            this.transform.rotation = _rotation;
        }
    }

    public Quaternion GetRotationTowardsTransform(Transform _referenceTransform, Transform _target)
    {
        Vector3 _direction = (_target.position - _referenceTransform.position).normalized;

        Quaternion _rotationToTarget = Quaternion.LookRotation(_direction);

        return Quaternion.Slerp(_referenceTransform.rotation, _rotationToTarget, Time.deltaTime * rotationSpeed);
    }

    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
