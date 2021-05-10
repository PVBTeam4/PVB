using Gun;
using UnityEngine;

namespace Enemy
{
    /// <summary>
    /// this class causes the enemey to move forward to the target
    ///  before it moves it rotate to the target
    /// it set the enemy as the same y position of the target
    /// </summary>
    public class EnemyAI : MonoBehaviour
    {
        public string playerTag;
        
        [SerializeField]
        // speed of Enemy 
        private float speed = 2f;
        // damage when collide target
        [SerializeField]
        private float damage;
        //get transform of target
        private Transform _targetTransform;

        // Start is called before the first frame update
        /// <summary>
        /// call this functions once
        /// </summary>
        private void OnEnable()
        {
            SetTargetTransform();
            SetEnemyPosition();
            RotateToTarget();
        }

        private void SetTargetTransform()
        {
            _targetTransform = GameObject.FindWithTag(playerTag).transform;
        }

        /// <summary>
        /// set the enemy's current y position to be the same as the target
        /// </summary>
        private void SetEnemyPosition()
        {
            transform.position = new Vector3(transform.position.x, _targetTransform.position.y, transform.position.z);
        }

        /// <summary>
        /// Rotates to the target
        /// </summary>
        private void RotateToTarget()
        {
            transform.LookAt(_targetTransform);
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

        public void DestroyEnemy()
        {
            Destroy(gameObject);
            // TODO Particle
        }

        private void OnTriggerEnter(Collider otherCollider)
        {
            if (!otherCollider.tag.Equals(playerTag)) return;
            PlayerHealth playerHealth = otherCollider.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth == null) return;
            playerHealth.DamageBy(damage);
            DestroyEnemy();
        }
    }
}
