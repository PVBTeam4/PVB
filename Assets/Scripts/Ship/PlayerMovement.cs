using UnityEngine;

namespace Ship
{
    /// <summary>
    /// Controlls the player movement
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Ship Movement Variables")]

        [SerializeField]
        // This variable is so the values in the inspector wont get too large
        private float forceScale = 1000;

        [SerializeField]
        // 
        private float trust = 60;

        [SerializeField]
        // Dont let the ship go faster than this
        private float maxVelocity = 100;

        [SerializeField]
        // How fast the ship will turn
        private float turningSpeed = 10;

        [SerializeField]
        // How faster the ship will turn when it goes fast
        private float turningSpeedScaleByVelocity = 0.5f;

        // Rigidbody of the player
        private Rigidbody _rigidbody;

        [Header("Visual Model")]

        // Transform of the boat model that we will tilt 
        private Transform _boatModelTransform;

        [SerializeField]
        // How much the ship will tilt forward or backwards when the ship moves in that opposite direction
        private float modelTiltScaleForward = 1.3f;

        [SerializeField]
        // How much the ship will tilt sidewards when the ship rotates in that opposite direction
        private float modelTiltScaleSide = 4f;

        // Start is called before the first frame update
        void Start()
        {
            // Get the components we need
            _rigidbody = GetComponent<Rigidbody>();

            // Get the transform of the boat model
            _boatModelTransform = gameObject.transform.GetComponentInChildren<Transform>();
        }

        // FixedUpdate to calculate physics
        void FixedUpdate()
        {
            // Rotate the ship using the Horizontal input axis
            RotatePlayer(turningSpeed, UnityEngine.Input.GetAxis("Horizontal"));

            // Move the ship in the direction its heading
            MovePlayerInDirection(trust, maxVelocity, UnityEngine.Input.GetAxis("Vertical"));
        }

        // Update is called once per frame
        private void Update()
        {
            // Run the effects in the update to save processing power

            SetDragByInput(UnityEngine.Input.GetAxis("Vertical"));

            // Tilting effects
            TileShipSidewards();

            TileShipForward();
        }

        /// <summary>
        /// Rotates the player
        /// </summary>
        /// <param name="_turningSpeed">How fast it needs to turn</param>
        /// <param name="_directionScale">-1 is Left. 1 Is Right</param>
        private void RotatePlayer(float _turningSpeed, float _directionScale)
        {
            // Scale the turning speed
            _turningSpeed *= forceScale;

            // The boat will turn faster by the magnitude of the velocity 
            float _turningSpeedDelta = _turningSpeed * (_rigidbody.velocity.magnitude * turningSpeedScaleByVelocity);
            float angle = (_turningSpeedDelta * _directionScale) * Time.fixedDeltaTime;

            Vector3 rotationVector = new Vector3(0, angle, 0);

            // Rotate the rigidbody by force
            _rigidbody.AddRelativeTorque(rotationVector);
        }

        /// <summary>
        /// Move the player in the direction it's facing
        /// </summary>
        /// <param name="_thrust">Amount of force it will be applied</param>
        /// <param name="_thrustScale">-1 is Backwards. 1 Is Forward</param>
        private void MovePlayerInDirection(float _thrust, float _maxVelocity, float _thrustScale)
        {
            // Scale the thrust force
            _thrust *= forceScale;

            // Move the player in the direction it's facing
            _rigidbody.AddRelativeForce((Vector3.forward * _thrust * Time.fixedDeltaTime) * _thrustScale);

            // Clamp (Dont let the value exeed) the velocity so it does not go faster
            Vector3 _maxVelocityVector = new Vector3(_maxVelocity, _maxVelocity, _maxVelocity);
            _rigidbody.velocity.Clamp(_maxVelocityVector.Multiply(-1), _maxVelocityVector);
        }

        /// <summary>
        /// Adjust the drag amount of the Rigidbody by the input scale
        /// </summary>
        /// <param name="inputScale"></param>
        private void SetDragByInput(float inputScale)
        {
            if (inputScale > 0)// When the boat starts to move, create a bit more drag
            {
                _rigidbody.drag = 0.75f;
            }
            else if (inputScale == 0)// When there is no input, let the ship slide more
            {
                _rigidbody.drag = 0.35f;
            }
            else if (inputScale < 0)// When backing up, let the ship drag more
            {
                _rigidbody.drag = 1.3f;
            }
        }

        /// <summary>
        /// Tilts the ship sidewards by the sidewards (X axis) velocity
        /// </summary>
        private void TileShipSidewards()
        {
            float _scale = -modelTiltScaleSide;
            float _angulatVelocity = Mathf.Abs(_rigidbody.angularVelocity.y);
            float _sideVelocity = _boatModelTransform.InverseTransformDirection(_rigidbody.velocity).x * _scale;

            _boatModelTransform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, _sideVelocity);
        }

        /// <summary>
        /// Tilts the ship forward by the forward (Z axis) velocity
        /// </summary>
        private void TileShipForward()
        {
            float _scale = modelTiltScaleForward;
            float _forwardVelocity = -_boatModelTransform.InverseTransformDirection(_rigidbody.velocity).z * _scale;

            _boatModelTransform.rotation = Quaternion.Euler(_forwardVelocity, transform.eulerAngles.y, _boatModelTransform.eulerAngles.z);
        }
    }
}
