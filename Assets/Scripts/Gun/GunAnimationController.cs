using System;
using UnityEngine;
using Utils;

namespace Gun
{
    [Serializable]
    public class GunMaterials
    {
        [SerializeField] private Material defaultMaterial, cooldownMaterial;
        public Material DefaultMaterial => defaultMaterial;
        public Material CooldownMaterial => cooldownMaterial;
    }
    
    public class GunAnimationController : MonoBehaviour
    {
        private static readonly int OverheatingStateName = Animator.StringToHash("Overheating");
        
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private SkinnedMeshRenderer gunSkinnedMeshRenderer;

        [SerializeField]
        private Transform bulletSpawnLocation;

        // Gun Materials
        [SerializeField]
        private GunMaterials gunMaterials;
        [SerializeField]
        private float materialLerpSpeed;
        private float _materialLerpValue;
        private bool _hadCooldown;

        private void Update()
        {
            UpdateGunMaterial();
        }

        private void UpdateGunMaterial()
        {
            bool inCooldown = animator.GetBool(OverheatingStateName);
            if (inCooldown)
            {
                if (!_hadCooldown)
                {
                    // Reset mat Lerp value
                    _materialLerpValue = 0;
                }
                _materialLerpValue += Time.deltaTime * materialLerpSpeed;
                LerpGunMaterials(_materialLerpValue);
            }
            else
            {
                if (_hadCooldown)
                {
                    // Reset mat Lerp value
                    _materialLerpValue = 1;
                }
                _materialLerpValue -= Time.deltaTime * materialLerpSpeed;
                LerpGunMaterials(_materialLerpValue);
            }
            _hadCooldown = inCooldown;
        }

        private void LerpGunMaterials(float lerpValue)
        {
            _materialLerpValue = Mathf.Clamp(lerpValue, 0, 1);
            gunSkinnedMeshRenderer.material.Lerp(gunMaterials.DefaultMaterial, gunMaterials.CooldownMaterial, _materialLerpValue);
        }

        public void PlayShootAnimation()
        {
            animator.Play("Shoot");
        }

        public void StartOverheatAnimation()
        {
            animator.SetBool(OverheatingStateName, true);
            
            // Spawn overheat particle
            ParticleUtil.Overheat.SpawnParticle(bulletSpawnLocation.position);
        }

        public void StopOverheatAnimation()
        {
            animator.SetBool(OverheatingStateName, false);
        }
    }
}
