using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this class causes the enemey to move forward to the target
///  before it moves it rotate to the target
/// it set the enemy as the same y position of the target
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
    //get transform of target
    private Transform targetTransform;

    // Start is called before the first frame update
    /// <summary>
    /// call this functions once
    /// </summary>
    private void Awake()
    {
        SetEnemyPosition();
        RotateToTarget();
    }

    /// <summary>
    /// set the enemy's current y position to be the same as the target
    /// </summary>
    private void SetEnemyPosition()
    {
        transform.position = new Vector3(transform.position.x, targetTransform.position.y, transform.position.z);
    }

    /// <summary>
    /// Rotates to the target
    /// </summary>
    private void RotateToTarget()
    {
        transform.LookAt(targetTransform);
    }

    void FixedUpdate()
    {
        MoveToTarget();
    }

    /// <summary>
    /// move forwards with speed * delta time
    /// </summary>
    private void MoveToTarget()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }





}
