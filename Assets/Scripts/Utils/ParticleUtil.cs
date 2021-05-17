using UnityEngine;

namespace Utils
{
    public static class ParticleUtil
    {
        private static ObjectPool ObjectPoolInstance => ObjectPool.Instance;

        public static GameObject SpawnParticle(string particleName, Vector3 position)
        {
            GameObject particleGameObject = ObjectPoolInstance.GetObject(particleName);
            particleGameObject.transform.position = position;
            particleGameObject.SetActive(true);
            return particleGameObject;
        }
        
    }
}
