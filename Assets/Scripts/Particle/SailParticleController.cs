using System;
using Ship;
using UnityEngine;

namespace Particle
{
    [Serializable]
    internal class VelocityParticleScaler
    {
        // Velocity Scaler
        public Transform gameObjectToScale;
        public bool enabled;
        // public float VelocityScalerMultiplier;
        public float minScale;
        public float maxScale;
        public float minThreshold;
        public float maxThreshold;
        public bool enableReversed;
    }

    [Serializable]
    internal class VelocityParticleActivation
    {
        public bool enabled;
        public float threshold;
        public bool enableReversed;
    }

    [Serializable]
    internal class SailParticleObject
    {
        // General
        public ParticleSystem particleSystem;

        // Velocity Activation
        public VelocityParticleActivation velocityParticleActivation;


        // Particle Scaler
        public VelocityParticleScaler velocityParticleScaler;
    }
    public class SailParticleController : MonoBehaviour
    {

        private PlayerMovement _playerMovement;
        
        [SerializeField]
        private SailParticleObject[] waterEngineParticles;

        [SerializeField]
        private SailParticleObject waterFrontParticle;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            HandleFrontParticles();
            HandleEngineParticles();
        }

        private void HandleFrontParticles()
        {
            // Velocity Activation
            HandleVelocityActivation(waterFrontParticle);
            
            // Velocity Scaler
            HandleVelocityScaler(waterFrontParticle);
        }

        private void HandleEngineParticles()
        {
            foreach (SailParticleObject waterEngineParticle in waterEngineParticles)
            {
                // Velocity Activation
                HandleVelocityActivation(waterEngineParticle);
            
                // Velocity Scaler
                HandleVelocityScaler(waterEngineParticle);
            }
        }

        private void HandleVelocityActivation(SailParticleObject sailParticleObject)
        {
            VelocityParticleActivation velocityParticleActivation = sailParticleObject.velocityParticleActivation;
            
            if (!velocityParticleActivation.enabled) return;
            
            float forwardDirection = _playerMovement.ForwardSpeed;

            if (velocityParticleActivation.enableReversed)
            {
                forwardDirection = Math.Abs(forwardDirection);
            }

            if (forwardDirection > velocityParticleActivation.threshold)
            {
                if (!sailParticleObject.particleSystem.IsAlive())
                    sailParticleObject.particleSystem.Play();
            }
            else if (sailParticleObject.particleSystem.IsAlive())
            {
                sailParticleObject.particleSystem.Stop();
            }
        }

        private void HandleVelocityScaler(SailParticleObject sailParticleObject)
        {
            VelocityParticleScaler velocityParticleScaler = sailParticleObject.velocityParticleScaler;
            if (!sailParticleObject.velocityParticleScaler.enabled) return;
            
            float forwardDirection = _playerMovement.ForwardSpeed;

            if (forwardDirection < 0 && !velocityParticleScaler.enableReversed) return;

            // Make positive
            forwardDirection = Math.Abs(forwardDirection);

            float normalizedSpeedWithinThreshold = Mathf.InverseLerp(velocityParticleScaler.minThreshold,
                velocityParticleScaler.maxThreshold, forwardDirection);

            float scale = Mathf.Lerp(velocityParticleScaler.minScale, velocityParticleScaler.maxScale,
                normalizedSpeedWithinThreshold);

            velocityParticleScaler.gameObjectToScale.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
