using UnityEngine;

namespace Ship
{
    /// <summary>
    /// Follows the given Target Transform via a Vector3.Lerp
    /// </summary>
    public class TargetFollower : MonoBehaviour 
    {
        [SerializeField]
        // The speed to lerp towards the Target position
        private float followSpeed = 7;

        [SerializeField]
        // Transform of the target
        private Transform targetTransform;

        void FixedUpdate()
        {
            // Follow the target by lerping towards it
            LerpToTarget(transform, targetTransform, followSpeed);
        }

        /// <summary>
        /// Lerp the given Transform towards the given target Transform
        /// </summary>
        /// <param name="_transform">Transform that needs to change</param>
        /// <param name="_targetTransform">Target Transform that </param>
        /// <param name="_followSpeed">Speed of the lerp towards, target</param>
        private void LerpToTarget(Transform _transform, Transform _targetTransform, float _followSpeed)
        {
            // Check if the targetTransform has been filled in
            if (_targetTransform)
            {
                // Lerp the given Transform towards the given target Transform
                _transform.position = Vector3.Lerp(_transform.position, _targetTransform.position, _followSpeed * Time.deltaTime);
            }
            else
            {
                // Else if it does not exits, log an Error
                Debug.LogError("Camera target to follow not found");
            }
        }
    }
}
