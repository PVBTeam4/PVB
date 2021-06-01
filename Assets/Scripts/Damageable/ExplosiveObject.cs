using System.Linq;
using UnityEngine;
using Utils;
using Utils.ShootScene;

namespace Damageable
{
    public class ExplosiveObject : MonoBehaviour
    {
        [SerializeField]
        private float explosionRange;
        [SerializeField]
        private float explosionDamage;

        private bool _exploded;

        private void OnEnable()
        {
            ResetExplosionState();
        }
        
        /// <summary>
        /// Detonate GameObject
        /// </summary>
        public void Detonate()
        {
            if (_exploded) return;
            _exploded = true;
            
            ParticleUtil.SpawnParticle(ParticleType.Explosion, transform.position);
            Destroy(gameObject);

            if (!(explosionDamage > 0)) return;

            foreach (GameObject damageableGameObject in DamageableUtil.GetDamageableGameObjectsWithinRange(transform.position, explosionRange)
                .Where(damageableGameObject => damageableGameObject != gameObject))
            {
                // If GameObject has DamageableObject component, do damage
                if (damageableGameObject.TryGetComponent(out DamageableObject damageableObject))
                {
                    damageableObject.Damage(explosionDamage);
                }
                // If GameObject has ExplosiveObject component, detonate
                else if (damageableGameObject.TryGetComponent(out ExplosiveObject explosiveObject))
                {
                    explosiveObject.Detonate();
                }
            }
        }
        
        /// <summary>
        /// Reset explosion state
        /// </summary>
        private void ResetExplosionState()
        {
            _exploded = false;
        }
    }
}
