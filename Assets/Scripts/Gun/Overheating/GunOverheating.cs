using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gun.Overheating
{
    public class GunOverheating : MonoBehaviour
    {
        [SerializeField]
        private float heatPerShot;
        [SerializeField]
        private float cooldownPerSecond;
        [SerializeField]
        private float overheatingTemperature;
        public float OverheatingTemperature => overheatingTemperature;


        private float _currentTemperature;

        public float CurrentTemperature => _currentTemperature;

        private bool _activeCooldown;

        [SerializeField]
        private UnityEvent cooldownFinished, cooldownStarted;

        private void Update()
        {
            CooldownTemperature();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Whether gun is overheated or not</returns>
        public void HeatGun()
        {
            float heatTemperature = heatPerShot;
            _currentTemperature = Math.Min(_currentTemperature + heatTemperature, overheatingTemperature);

            if (_currentTemperature == overheatingTemperature)
            {
                _activeCooldown = true;
                cooldownStarted?.Invoke();
            }
        }

        public bool HasCooldown()
        {
            return _activeCooldown;
        }

        /// <summary>
        /// Cool down temperature of gun
        /// </summary>
        private void CooldownTemperature()
        {
            float cooldownTemperature = Time.deltaTime * cooldownPerSecond;
            _currentTemperature = Math.Max(_currentTemperature - cooldownTemperature, 0);

            if (_activeCooldown && _currentTemperature == 0)
            {
                _activeCooldown = false;
                cooldownFinished?.Invoke();
            }
        }
    }
}
