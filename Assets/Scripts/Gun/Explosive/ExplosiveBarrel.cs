using Damageable;
using Properties.Tags;
using UnityEngine;
using Utils;

namespace Gun.Explosive
{
    public class ExplosiveBarrel : MonoBehaviour
    {
        [SerializeField, TagSelector]
        private string bulletTag = "Bullet";
        private void OnCollisionEnter(Collision other)
        {
            if (!other.collider.gameObject.CompareTag(bulletTag)) return;
            
            if (gameObject.TryGetComponent(out ExplosiveObject explosiveObject))
            {
                // Spawn Impact Particle
                ParticleUtil.ImpactBoot.SpawnParticle(other.contacts[0].point);
                // Pool bullet
                ObjectPool.Instance.PoolObject(other.collider.gameObject);
                // Detonate barrel
                explosiveObject.Detonate();
            }
        }
    }
}
