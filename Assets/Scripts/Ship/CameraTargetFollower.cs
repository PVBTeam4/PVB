using UnityEngine;

namespace Ship
{
    /// <summary>
    /// Follows the given Target Transform via a Vector3.Lerp
    /// </summary>
    public class CameraTargetFollower : MonoBehaviour 
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
            LerpToTarget();
        }

        /// <summary>
        /// Lerp the given Transform towards the given target Transform
        /// </summary>
        private void LerpToTarget()
        {
            // Check if the targetTransform has been filled in
            if (targetTransform)
            {
                // Lerp the given Transform towards the given target Transform
                transform.position = Vector3.Lerp(transform.position, targetTransform.position, followSpeed * Time.deltaTime);
            }
            else
            {
                // Else if it does not exits, log an Error
                Debug.LogError("Camera target to follow not found");
            }
        }
    }
}
