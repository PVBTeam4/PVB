using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    private float damage;
    [SerializeField]
    private Transform targetTransform;

    // Start is called before the first frame update
    private void Awake()
    {
        RotateToTarget();
    }

    private void RotateToTarget()
    {
        transform.LookAt(targetTransform);
    }

    void FixedUpdate()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }



}
