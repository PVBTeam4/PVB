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
                Vector3 collisionPoint = collision.contacts[0].point;

                ParticleUtil.ImpactWater.SpawnParticle(collisionPoint);

                //Play the sound
                FMODUnity.RuntimeManager.PlayOneShot("event:/Events/Kogel_Water", collisionPoint);
            }
        }
    }
}
