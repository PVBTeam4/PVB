using Properties.Tags;
using UnityEngine;
using Utils;

namespace Gun
{
    public class WaterImpact : MonoBehaviour
    {
        [SerializeField, TagSelector]
        private string bulletTag;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(bulletTag))
            {
                ParticleUtil.SpawnParticle("ImpactWater", other.transform.position);
            }
        }
    }
}
