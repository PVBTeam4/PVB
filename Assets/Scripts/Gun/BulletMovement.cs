using UnityEngine;

namespace Gun
{
    /// <summary>
    /// this class causes the bullet to move forward and
    /// when it collides or after time of lifespan the bullet will be destroyed
    /// this script wil be added to the prefab (BulletForShip)
    /// </summary>
    public class BulletMovement : MonoBehaviour
    {
        [SerializeField]
        // the speed of the bullet
        private float speed = 10;
        [SerializeField]
        // is the time of life until it dies
        private float lifeSpan = 3;

        private void Start()
        {
            SetLifeSpanTimer();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            ShootBulletForward();
        }

        /// <summary>
        /// Shoots the bullet forward, called each frame
        /// </summary>
        private void ShootBulletForward()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        /// <summary>
        ///if it interacts with an object, it will activate this function to destroy it itself
        /// </summary>
        private void OnTriggerEnter(Collider col)
        {   
            //Destroy sthe bullet after collision
            Destroy(gameObject);
        }
        /// <summary>
        /// Destroys the game object in the time of the lifeSpan
        /// </summary>
        private void SetLifeSpanTimer()
        {  
            Destroy(gameObject, lifeSpan);
        }
    }
}