using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    // is the time of life until it dies
    private float lifeSpan = 3;

    void Start()
    {   //Destroys the game object in the time of the lifeSpan
        Destroy(gameObject, lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {   // shoots the bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }

    void OnTriggerEnter(Collider col)
    {   //Destroy sthe bullet after collision
        Destroy(gameObject);

    }
}