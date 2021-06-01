using System;
using UnityEngine;
using UnityEngine.Events;

namespace Damageable
{
    public class DamageableObject : MonoBehaviour
    {
        [SerializeField]
        private float maxHealth;
        public float MaxHealth => maxHealth;
        
        // Event that will be triggered when objective gets damaged
        // <Current Health, Max Health>
        [SerializeField] private UnityEvent<float, float> onDamage;

        [SerializeField] private UnityEvent onDeath;

        private float _currentHealth;
        
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
        /// Damage object
        /// </summary>
        /// <param name="damage">Amount of damage to inflict</param>
        /// <returns>Remaining health</returns>
        public float Damage(float damage)
        {
            _currentHealth = Math.Max(_currentHealth - damage, 0);

            onDamage?.Invoke(_currentHealth, MaxHealth);
            
            if (_currentHealth == 0) OnDeath();
            
            return _currentHealth;
        }

        private void OnDeath()
        {
            onDeath?.Invoke();
            if (gameObject.TryGetComponent(out ExplosiveObject explosiveObject))
            {
                explosiveObject.Detonate();
            }
        }
    }
}
