using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    [Serializable]
    public class ParticleType : Object
    {
        public string ParticleName { get; }

        public ParticleType(string particleName)
        {
            ParticleName = particleName;
        }
        
        private ObjectPool ObjectPoolInstance => ObjectPool.Instance;

        public GameObject SpawnParticle(Vector3 position)
        {
            return SpawnParticle(ParticleName, position);
        }
        
        private GameObject SpawnParticle(string particleName, Vector3 position)
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
    public static class ParticleUtil
    {
        public static readonly ParticleType ExplosionBarrels = new ParticleType("ExplosionBarrels");
        public static readonly ParticleType Explosion = new ParticleType("Explosion");
        public static readonly ParticleType ImpactStone = new ParticleType("ImpactStone");
        public static readonly ParticleType BulletForShip = new ParticleType("BulletForShip");
        public static readonly ParticleType ImpactWater = new ParticleType("ImpactWater");
        public static readonly ParticleType ImpactBoot = new ParticleType("ImpactBoot");
        public static readonly ParticleType MuzzleFlash = new ParticleType("MuzzleFlash");
        public static readonly ParticleType CranePoof = new ParticleType("CranePoof");
        public static readonly ParticleType Overheat = new ParticleType("Overheat");
    }
}
