using Properties.Tags;
using UnityEngine;
using Utils;

namespace Gun.VFX
{
    public class WaterImpact : MonoBehaviour
    {
        [SerializeField, TagSelector]
        private string waterColliderTag;
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.CompareTag(waterColliderTag))
            {
                ParticleUtil.SpawnParticle(ParticleType.ImpactWater, collision.contacts[0].point);
            }
        }
    }
}
