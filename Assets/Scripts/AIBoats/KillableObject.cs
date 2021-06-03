using Damageable;
using Gun;
using Properties.Tags;
using UnityEngine;
using Utils;

namespace AIBoats
{
    /// <summary>
    /// Monobehaviour component meant for boats that need to be destroyable by the player
    /// </summary>
    public class KillableObject : MonoBehaviour
    {
        [SerializeField]
        private float damage;

        // Tag name of bullet
        [SerializeField, TagSelector]
        private string bulletTag;
        [SerializeField, TagSelector]
        private string targetTag;

        private DamageableObject _damageableObject;
        public DamageableObject DamageableObject => _damageableObject;
        
        private void OnEnable()
        {
            if (!gameObject.TryGetComponent(out DamageableObject damageableObject))
            {
                Debug.LogError("DamageableObject component is missing!");
                return;
            }
            _damageableObject = damageableObject;
        }

        /// <summary>
        /// Damage objective:
        /// subtract health;
        /// complete objective, if health is zero;
        /// </summary>
        /// <param name="damageTaken"></param>
        public void DamageBy(float damageTaken)
        {
            if (_damageableObject == null)
            {
                Debug.LogError("DamageableObject component is missing!");
                return;
            }

            _damageableObject.Damage(damageTaken);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(bulletTag))
            {
                BulletMovement bulletMovement = other.gameObject.GetComponent<BulletMovement>();
                if (bulletMovement == null) return;
                DamageBy(bulletMovement.damage);

                // Spawn Impact Particle
                ParticleUtil.ImpactBoot.SpawnParticle(bulletMovement.transform.position);
            }
            else if (other.gameObject.CompareTag(targetTag))
            {
                PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth == null) return;
                playerHealth.DamageBy(damage);

                // Kill boat
                DamageBy(_damageableObject.MaxHealth);
            }
        }
    }
}
