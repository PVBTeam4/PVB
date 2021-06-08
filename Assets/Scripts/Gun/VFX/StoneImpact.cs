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
                Vector3 collisionPoint = collision.contacts[0].point;

                ParticleUtil.ImpactStone.SpawnParticle(collisionPoint);

                //Play the sound
                FMODUnity.RuntimeManager.PlayOneShot("event:/Events/Kogel_Rots", collisionPoint);
            }
        }
    }
}
