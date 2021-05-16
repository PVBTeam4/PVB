using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gun
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        private float _currentHealth;

        [SerializeField] private UnityEvent<float, float> onDamage;
        [SerializeField] private UnityEvent onDeath;
        
        void OnEnable()
        {
            SetCurrentHealth();
        }

        public void DamageBy(float damage)
        {
            _currentHealth -= damage;
            _currentHealth = Math.Max(0, _currentHealth);
            
            onDamage?.Invoke(_currentHealth, maxHealth);
            
            if (_currentHealth == 0)
                onDeath?.Invoke();
        }

        private void SetCurrentHealth()
        {
            _currentHealth = maxHealth;
        }
    }
}
