using UnityEngine;

namespace Utils
{
    public static class ParticleUtil
    {
        private static ObjectPool ObjectPoolInstance => ObjectPool.Instance;

        public static GameObject SpawnParticle(string particleName, Transform transform)
        {
            GameObject particleGameObject = ObjectPoolInstance.GetObject(particleName);
            
            particleGameObject.transform.position = transform.position;
            particleGameObject.transform.rotation = transform.rotation;
            particleGameObject.transform.localScale = transform.localScale;
            
            particleGameObject.SetActive(true);
            return particleGameObject;
        }
        
    }
}
