using System;
using UnityEngine;

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
            Debug.Log(_materialLerpValue);
            Material material = gunSkinnedMeshRenderer.materials[0];
            Color color = material.color;
            color.a = _materialLerpValue;
            material.color = color;
        }

        public void PlayShootAnimation()
        {
            animator.Play("Shoot");
        }

        public void StartOverheatAnimation()
        {
            animator.SetBool(OverheatingStateName, true);
        }

        public void StopOverheatAnimation()
        {
            animator.SetBool(OverheatingStateName, false);
        }
    }
}
