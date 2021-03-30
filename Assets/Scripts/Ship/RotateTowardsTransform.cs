using UnityEngine;

namespace Ship
{
    /// <summary>
    /// Rotates the coupled Transform towards the given target's Transform
    /// </summary>
    public class RotateTowardsTarget : MonoBehaviour
    {
        [SerializeField]
        // The Transform the GameObject will rotate towards
        private Transform targetTransform;

        [SerializeField]
        // How fast the object shall rotate towards the target
        private float rotationSpeed = 1f;

        [SerializeField]
        // If you'll set an Axis to 0, it will not rotate that Axis
        private Vector3 rotationSpeedScale = new Vector3(1,1,1);

        // Update is called once per frame
        void Update()
        {
            // Update the rotation of the Transform
            RotateTowardsTargetTransform(this.transform, targetTransform);
        }

        /// <summary>
        /// Rotates the given Transform towards the given Target Transform
        /// </summary>
        private void RotateTowardsTargetTransform(Transform _referenceTransform, Transform _targetTransform)
        {
            // If the target exist, rotate towards it
            if (_targetTransform)
            {
                // Get the rotation towards the target
                Quaternion _rotation = GetSlerpedRotationTowardsTransform(_referenceTransform, _targetTransform, rotationSpeed);

                // Rotate this GameObject towards the target
                _referenceTransform.rotation = _rotation.Multiply(rotationSpeedScale);
            }
        }

        /// <summary>
        /// Returns a Slerped Quaternion 
        /// </summary>
        /// <param name="_referenceTransform"></param>
        /// <param name="_target"></param>
        /// <returns>Quaternion</returns>
        public Quaternion GetSlerpedRotationTowardsTransform(Transform _referenceTransform, Transform _targetTransform, float _rotationSpeed)
        {
            // Get a normalized Vector3 of the two positions
            Vector3 _direction = (_targetTransform.position - _referenceTransform.position).normalized;

            // Look towards the direction
            Quaternion _rotationToTarget = Quaternion.LookRotation(_direction);

            // Slerp the rotation to the target
            return Quaternion.Slerp(_referenceTransform.rotation, _rotationToTarget, Time.deltaTime * _rotationSpeed);
        }

        /// <summary>
        /// Returns the angle between two Vector3's
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>float</returns>
        private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }
    }
}
