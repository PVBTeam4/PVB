using Gun;
using Properties.Tags;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace TaskSystem.Objectives
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

        // Max health of objective
        public float maxHealth;

        // Current health of objective
        private float _currentHealth;

        // Event that will be triggered when objective gets damaged
        // <Current Health, Max Health>
        [SerializeField] private UnityEvent<float, float> onDamage;

        [SerializeField] private UnityEvent onDeath;

        /// <summary>
        /// Called when GameObject, gets enabled.
        /// </summary>
        private void OnEnable()
        {
            SetCurrentToMaxHealth();
        }

        /// <summary>
        /// Set current health, to max health
        /// </summary>
        private void SetCurrentToMaxHealth()
        {
            _currentHealth = maxHealth;
        }

        /// <summary>
        /// Damage objective:
        /// subtract health;
        /// complete objective, if health is zero;
        /// </summary>
        /// <param name="damageTaken"></param>
        /// <param name="handleTaskCompletion"></param>
        public void DamageBy(float damageTaken, bool handleTaskCompletion)
        {
            // Remove health, by damage amount
            _currentHealth -= damageTaken;
            // Max currentHealth to 0, so we don't have negative health
            _currentHealth = Mathf.Max(_currentHealth, 0);

            onDamage?.Invoke(_currentHealth, maxHealth);

            // Complete objective, if health is zero
            if (_currentHealth == 0)
            {
                onDeath?.Invoke();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(bulletTag))
            {
                BulletMovement bulletMovement = other.gameObject.GetComponent<BulletMovement>();
                if (bulletMovement == null) return;
                DamageBy(bulletMovement.damage, true);

                // Spawn Impact Particle
                ParticleUtil.SpawnParticle("ImpactBoot", bulletMovement.transform.position);
            }
            else if (other.gameObject.CompareTag(targetTag))
            {
                PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth == null) return;
                playerHealth.DamageBy(damage);

                // Kill boat
                DamageBy(_currentHealth, false);
            }
        }
    }
}