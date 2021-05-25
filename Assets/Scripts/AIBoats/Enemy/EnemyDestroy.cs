using UnityEngine;
using Utils;

namespace Enemy
{
    public class EnemyDestroy : MonoBehaviour
    {
        public void DestroyEnemy()
        {
            Vector3 position = transform.position;
            
            Destroy(gameObject);

            // Spawn particle
            ParticleUtil.SpawnParticle("Explosion", position);
        }
    }
}
