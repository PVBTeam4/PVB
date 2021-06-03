using Gun;
using Properties.Tags;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace TaskSystem.Objectives
{
    /// <summary>
    /// Component that will handle the damage done by Cannon Tool
    /// </summary>
    public class KraneObjective : Objective
    {
        [SerializeField]
        private float damage;

        public float Damage
        {
            get => damage;
        }

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
                
                if (handleTaskCompletion)
                {
                    CompleteObjective();
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.CompareTag(bulletTag))
            {
                BulletMovement bulletMovement = collision.collider.gameObject.GetComponent<BulletMovement>();
                if (bulletMovement == null) return;
                DamageBy(bulletMovement.damage, true);

                // Spawn Impact Particle
                ParticleUtil.ImpactBoot.SpawnParticle(bulletMovement.transform.position);
            } else if (collision.collider.gameObject.CompareTag(targetTag))
            {
                PlayerHealth playerHealth = collision.collider.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth == null) return;
                playerHealth.DamageBy(damage);
                
                // Kill boat
                DamageBy(_currentHealth, false);
            }
        }
    }
}
