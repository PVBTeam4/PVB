using UnityEngine;
using Utils;

namespace Particle
{
    public class ParticleObject : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        void Awake()
        {
            _particleSystem = GetParticleSystem();
        }

        void Update()
        {
            HandleParticleFinished();
        }

        private void HandleParticleFinished()
        {
            if (_particleSystem && _particleSystem.IsAlive()) return;
            ObjectPool.Instance.PoolObject(gameObject);
        }

        private ParticleSystem GetParticleSystem()
        {
            ParticleSystem system = GetComponent<ParticleSystem>();

            if (system != null) return system;

            system = GetComponentInChildren<ParticleSystem>();

            return system;
        }
    }
}
