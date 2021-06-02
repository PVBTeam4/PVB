using UnityEngine;

namespace Utils
{
    public class ParticleType
    {
        public static ParticleType Explosion => new ParticleType("Explosion");
        public static ParticleType ImpactStone => new ParticleType("ImpactStone");
        public static ParticleType BulletForShip => new ParticleType("BulletForShip");
        public static ParticleType ImpactWater => new ParticleType("ImpactWater");
        public static ParticleType ImpactBoot => new ParticleType("ImpactBoot");
        public static ParticleType MuzzleFlash => new ParticleType("MuzzleFlash");
        public static ParticleType CranePoof => new ParticleType("CranePoof");

        public string Name { get; }

        private ParticleType(string particleName)
        {
            Name = particleName;
        }
    }
    public static class ParticleUtil
    {
        private static ObjectPool ObjectPoolInstance => ObjectPool.Instance;

        public static GameObject SpawnParticle(ParticleType particleType, Vector3 position)
        {
            return SpawnParticle(particleType.Name, position);
        }
        
        private static GameObject SpawnParticle(string particleName, Vector3 position)
        {
            GameObject particleGameObject = ObjectPoolInstance.GetObject(particleName);
            if (particleGameObject == null)
            {
                Debug.LogError("Particle is " + particleName);
                return null;
            }
            particleGameObject.transform.position = position;
            particleGameObject.SetActive(true);
            return particleGameObject;
        }
        
    }
}
