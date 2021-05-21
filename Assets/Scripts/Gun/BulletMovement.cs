using System.Collections;
using UnityEngine;
using Utils;

namespace Gun
{
    /// <summary>
    /// this class causes the bullet to move forward and
    /// when it collides or after time of lifespan the bullet will be destroyed
    /// this script wil be added to the prefab (BulletForShip)
    /// </summary>
    public class BulletMovement : MonoBehaviour
    {
        public float damage;
        
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
            string tag = col.gameObject.tag;

            if (tag == "WaterCollider" || tag == "Player") return;

            // Pool the bullet after collision
            PoolBullet();
        }

        private void OnCollisionEnter(Collision collision)
        {
            string tag = collision.gameObject.tag;

            if (tag == "WaterCollider" || tag == "Player") return;

            GameObject impact = ParticleUtil.SpawnParticle("ImpactStone", collision.GetContact(0).point);

            // Pool the bullet after collision
            PoolBullet();
        }

        /// <summary>
        /// Destroys the game object in the time of the lifeSpan
        /// </summary>
        private void SetLifeSpanTimer()
        {

            StartCoroutine(LifeSpanTimer());
        }

        private IEnumerator LifeSpanTimer()
        {
            yield return new WaitForSeconds(lifeSpan);
            PoolBullet();
        }

        private void PoolBullet()
        {
            if (ObjectPool.Instance == null)
            {
                Destroy(gameObject);
                return;
            }
            ObjectPool.Instance.PoolObject(gameObject);
        }
    }
}