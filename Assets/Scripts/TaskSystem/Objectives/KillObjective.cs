using UnityEngine;

namespace TaskSystem.Objectives
{
    /// <summary>
    /// Component that will handle the damage done by Cannon Tool
    /// </summary>
    public class KillObjective : Objective
    {
        // Max health of objective
        [SerializeField]
        private float maxHealth;
        
        // Current health of objective
        private float _currentHealth;

        /// <summary>
        /// Called when GameObject, gets enabled.
        /// </summary>
        private void OnEnable()
        {
            SetCurrentHealth();
        }

        /// <summary>
        /// Set current health, to max health
        /// </summary>
        private void SetCurrentHealth()
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
            
            // Complete objective, if health is zero
            if (_currentHealth == 0)
                CompleteObjective();
        }
    }
}
