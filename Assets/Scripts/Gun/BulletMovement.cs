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
        /// Collision with the environment (Watch out for random colliders. In ShootingScene objects: Floor, rr, NavMeshFloor. You can test it with: print(col.gameObject.name);)
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            // Pool the bullet after collision
            PoolBullet();
        }
        /// <summary>
        /// fix for enemy/refugee bullet set to false
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            gameObject.SetActive(false);
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