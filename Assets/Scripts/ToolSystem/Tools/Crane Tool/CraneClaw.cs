using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneClaw : MonoBehaviour
{
    [SerializeField]
    private string liftObjectTag = "LiftObject";

    public bool canCheckCollision = false;

    private Action<GameObject> collisionEvent;
    public Action<GameObject> CollisionEvent { get => collisionEvent; set => collisionEvent = value; }


    // When the claw collides with an LiftObject
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(liftObjectTag))
        {
            CollisionEvent.Invoke(other.gameObject);
        }
    }
}
