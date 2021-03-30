using UnityEngine;

namespace Ship
{
    /// <summary>
    /// Follow target transform
    /// </summary>
    public class TargetFollower : MonoBehaviour 
    {
        // Follow speed
        [SerializeField]
        private float followSpeed;

        // Transform to follow
        [SerializeField]
        private Transform targetTransform;

        void FixedUpdate()
        {
            this.LerpTowardsTarget();
        }

        /// <summary>
        /// Lerp current transform towards target
        /// </summary>
        private void LerpTowardsTarget()
        {
            transform.position = Vector3.Lerp(transform.position, targetTransform.position, followSpeed * Time.deltaTime);
        }
    }
}
