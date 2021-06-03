using Properties.Tags;
using UnityEngine;
using Utils;

namespace Gun.VFX
{
    public class StoneImpact : MonoBehaviour
    {
        [SerializeField, TagSelector]
        private string stoneColliderTag;
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.CompareTag(stoneColliderTag))
            {
                ParticleUtil.ImpactStone.SpawnParticle(collision.contacts[0].point);
            }
        }
    }
}
