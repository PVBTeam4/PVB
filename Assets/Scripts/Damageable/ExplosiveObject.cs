using System.Linq;
using UnityEngine;
using Utils;
using Utils.ShootScene;

namespace Damageable
{
    public class ExplosiveObject : MonoBehaviour
    {
        // [SerializeField, ParticleTypeSelector] private ParticleType particleType;

        [SerializeField]
        private bool useBarrelExplosionParticle;
        
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

            //Play the sound
            FMODUnity.RuntimeManager.PlayOneShot("event:/Events/Explosie", transform.position);

            ParticleType particleType = useBarrelExplosionParticle ? ParticleUtil.ExplosionBarrels : ParticleUtil.Explosion;
            
            particleType.SpawnParticle(transform.position);
            
            Destroy(gameObject);

            if (!(explosionDamage > 0)) return;

            foreach (GameObject damageableGameObject in DamageableUtil.GetDamageableGameObjectsWithinRange(transform.position, explosionRange)
                .Where(damageableGameObject => damageableGameObject != gameObject))
            {
                // If GameObject has DamageableObject component, do damage
                if (damageableGameObject.TryGetComponent(out DamageableObject damageableObject))
                {
                    if (!damageableObject.CompareTag("ShipsKilledUI"))
                    damageableObject.Damage(explosionDamage);
                }
                // If GameObject has ExplosiveObject component, detonate
                else if (damageableGameObject.TryGetComponent(out ExplosiveObject explosiveObject))
                {
                    if (!explosiveObject.CompareTag("Explosive"))
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
