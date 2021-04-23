using System;
using Gun;
using UnityEngine;
using UnityEngine.Events;

namespace TaskSystem.Objectives
{
    /// <summary>
    /// Component that will handle the damage done by Cannon Tool
    /// </summary>
    public class KillObjective : Objective
    {
        // Tag name of bullet
        private const string BulletTag = "Bullet";

        // Max health of objective
        [SerializeField]
        private float maxHealth;
        
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
        /// <param name="damage"></param>
        public void DamageBy(float damage)
        {
            // Remove health, by damage amount
            _currentHealth -= damage;
            // Max currentHealth to 0, so we don't have negative health
            _currentHealth = Mathf.Max(_currentHealth, 0);
            
            onDamage?.Invoke(_currentHealth, maxHealth);
            
            // Complete objective, if health is zero
            if (_currentHealth == 0)
            {
                onDeath?.Invoke();
                CompleteObjective();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(BulletTag))
            {
                BulletMovement bulletMovement = other.gameObject.GetComponent<BulletMovement>();
                if (bulletMovement == null) return;
                DamageBy(bulletMovement.damage);
            }
        }
    }
}
