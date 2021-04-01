using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Sets the speed of the projectile
    [SerializeField]
    private float bulletSpeed = 10;

    // Sets the time in seconds before the object is removed from the scene
    [SerializeField]
    private float lifeSpan = 3;

    /// <summary>
    /// Destroys the gameobject a few seconds after being instantiated
    /// </summary>
    void Start()
    { 
        destroyBullet(lifeSpan);
    }

    /// <summary>
    /// Updates the movement of the bullet for every frame of its lifespan
    /// </summary>
    void Update()
    {  
        moveBullet();
    }

    /// <summary>
    /// Moves the projectile in a 'forward' direction
    /// </summary>
    private void moveBullet()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Destroys/removes the bullet based on a given amount of time
    /// </summary>
    private void destroyBullet(float timeInSeconds = 0)
    {
        Destroy(gameObject, timeInSeconds);
    }

    /// <summary>
    /// Destroys the object on collision
    /// </summary>
    void OnTriggerEnter(Collider col)
    {
        destroyBullet();
    }
}