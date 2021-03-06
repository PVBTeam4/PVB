using System;
using Properties.Tags;
using TaskSystem.Objectives;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gun
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField, TagSelector] private string damageableTag;
        
        [SerializeField] private float maxHealth;
        private float _currentHealth;

        [SerializeField] private UnityEvent<float, float> onDamage;
        [SerializeField] private UnityEvent onDeath;

        [SerializeField] private float cameraShakeIntensity = 4;

        void OnEnable()
        {
            SetCurrentHealth();
        }

        public void DamageBy(float damage)
        {
            //Play the sound
            FMODUnity.RuntimeManager.PlayOneShot("event:/Events/Botsing", transform.position);

            _currentHealth -= damage;
            _currentHealth = Math.Max(0, _currentHealth);
            
            onDamage?.Invoke(_currentHealth, maxHealth);

            EZCameraShake.CameraShaker.Instance.ShakeOnce(cameraShakeIntensity, 4, 0.2f, 0.2f);

            if (_currentHealth <= 50)
                GameObject.FindGameObjectWithTag("Vignet").GetComponent<Image>().enabled = true;

            if (_currentHealth == 0)
                onDeath?.Invoke();

            
        }

        private void SetCurrentHealth()
        {
            _currentHealth = maxHealth;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (!collider.gameObject.CompareTag(damageableTag)) return;
            KillObjective killObjective = collider.gameObject.GetComponent<KillObjective>();
            if (killObjective == null) return;
            DamageBy(killObjective.Damage);
            
            // Kill boat
            killObjective.DamageBy(killObjective.maxHealth, false);
        }
    }
}
