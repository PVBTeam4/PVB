using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gun.Overheating
{
    public class OverheatingMeter : MonoBehaviour
    {
        [SerializeField] private GunOverheating gunOverheating;
        
        [SerializeField]
        private Transform needlePivot;

        [SerializeField, Header("Rotation Values")]
        private float minRotation;
        [SerializeField]
        private float maxRotation;
        [SerializeField]
        private float maxOverheatedRotation;

        [SerializeField] private float needleSpeed;

        private bool _movingUp;

        [SerializeField, Header("Cooldown Needle Values")]
        private Vector2 randomToleranceRange;
        [SerializeField]
        private Vector2 randomCooldownNeedleSpeed;
        
        private void Update()
        {
            UpdateOverheatingMeter();
        }

        private void UpdateOverheatingMeter()
        {
            if (gunOverheating.HasCooldown())
            {
                UpdateCooldownOverheating();
            }
            else
            {
                UpdateCooldownAmount();
            }
        }

        private void UpdateCooldownAmount()
        {
            float targetPercentage = Mathf.InverseLerp(0, gunOverheating.OverheatingTemperature, gunOverheating.CurrentTemperature);
            Vector3 eulerAngles = needlePivot.eulerAngles;
            float newZRotation = Mathf.LerpAngle(minRotation, maxRotation, targetPercentage);
            Vector3 newEulerAngles = Vector3.MoveTowards(eulerAngles, new Vector3(eulerAngles.x, eulerAngles.y, newZRotation), Time.deltaTime * needleSpeed);

            // Lock at min rotation
            if (newEulerAngles.z < maxOverheatedRotation && newEulerAngles.z >= 0) newEulerAngles.Set(newEulerAngles.x, newEulerAngles.y, minRotation);

            needlePivot.eulerAngles = newEulerAngles;
        }

        private void UpdateCooldownOverheating()
        {
            Vector3 eulerAngles = needlePivot.eulerAngles;
            float newZRotation = Mathf.LerpAngle(eulerAngles.z, _movingUp ? maxRotation : maxOverheatedRotation, Time.deltaTime * Random.Range(randomCooldownNeedleSpeed.x, randomCooldownNeedleSpeed.y));
            needlePivot.eulerAngles = new Vector3(eulerAngles.x, eulerAngles.y, newZRotation);

            float tolerance = Random.Range(randomToleranceRange.x, randomToleranceRange.y);
            if (Mathf.Abs(newZRotation - Math.Abs(maxRotation)) < tolerance)
            {
                _movingUp = false;
            } else if (Mathf.Abs(newZRotation - maxOverheatedRotation) < tolerance)
            {
                _movingUp = true;
            }
        }
    }
}
