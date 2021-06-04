using Input;
using TaskSystem.Tasks;
using Utils;
using UnityEngine;

namespace ToolSystem.Tools.Crane_Tool
{
    public class CraneTool : Tool
    {
        private bool canInteract;
        public bool CanInteract { get => canInteract; set => canInteract = value; }


        [SerializeField]
        private Camera usedCamera;

        private float liftingProcess;

        [SerializeField]
        private Vector3 offset;

        [Header("Mast rotation")]

        [Header("Transform limitations")]

        [SerializeField]
        private float maxAngle = 30;// In degrees

        [SerializeField]
        private float minRadius = 1;

        [SerializeField]
        private float maxRadius = 10;

        [Header("Arm / Hook")]

        [SerializeField]
        private float hookLiftStart = 0.2f;

        [SerializeField]
        private float hookLiftEnd = 10;

        [SerializeField]
        private float hookLiftSpeed = 3;

        [SerializeField]
        // How soon before it returns
        private float clawLiftMargin = 0.08f;

        // 0 Is idle, 1 = moving down, -1 = moving up
        private float isLiftingScale = 0;

        private GameObject coupeledObject = null;

        [Header("Mast rotation")]

        [SerializeField]
        private Transform originPositionTransform;

        [SerializeField]
        private Transform craneArm;

        [SerializeField]
        private Transform craneClaw;

        private Vector3 mousePosition;

        private CraneTask craneTask;

        private void Start()
        {
            InputManager.MouseMovementAction += UpdateMousePosition;

            InputManager.ButtonInputAction += LeftAction;

            // Reset the crane claw position
            Vector3 armPosition = craneArm.position;
            craneClaw.position = craneClaw.position.ChangeY(armPosition.y + hookLiftStart - clawLiftMargin);

            // Subscribe the OnClawCollisionEvent to the Collision event on the claw
            craneClaw.GetComponent<CraneClaw>().CollisionEvent += OnClawCollisionEvent;

            CraneBehaviour craneBehaviour = FindObjectOfType<CraneBehaviour>();
            if (craneBehaviour) craneBehaviour.CraneActivationEvent += (bool value) => { CanInteract = value; };

                // Get the crane task
                craneTask = FindObjectOfType<CraneTask>();

            if (!craneTask)
                Debug.Log("No object found with the CraneTask script. Try adding the 'CraneTaskManager' prefab");
        }

        private void UpdateMousePosition(Vector3 position)
        {
            HandleMovement(position);
        }

        /// <summary>
        /// Moves this object by mouse & raycast. And is clamped within a min/max radius
        /// </summary>
        /// <param name="position"></param>
        private void HandleMovement(Vector3 position)
        {
            if (usedCamera)
            {
                RaycastHit hit;
                //position.y -= -Screen.height / 2;
                //position.z = 5f;
                Ray ray = usedCamera.ScreenPointToRay(position);
                
                if (Physics.Raycast(ray, out hit))
                {
                    UpdatePosition(hit.point);
                }
            }
            else
            {
                Debug.LogError("No Camera found");
            }
        }

        private void UpdatePosition(Vector3 mousePosition)
        {
            if (originPositionTransform == null)
                return;

            Vector3 originPos = originPositionTransform.position;
            originPos.y = 0;

            // Get the position of the hit poing (Remove the originPos for clamping the magnitude)
            Vector3 hitPosition = mousePosition - originPos;

            // Set the position to the detected raycastpoint and clamp it
            Vector3 pos = hitPosition.ClampMagnitudeMinMax(minRadius, maxRadius);
            pos.y = 0;

            // Set the position to that of the hit position with the added origin position
            transform.position = originPos + pos;
        }

        private void LeftAction(ButtonInputType type, float amount)
        {
            if (amount > 0)
            {
                if (type.Equals(ButtonInputType.LeftMouse) && CanInteract)
                {
                    if (isLiftingScale == 0)
                        StartLiftingObject();
                }       
            }
        }

        /// <summary>
        /// Sets the isLiftingScale so it can drop down to the water
        /// </summary>
        private void StartLiftingObject()
        {
            isLiftingScale = 1;
        }

        /// <summary>
        /// When the claw reaches its start point
        /// </summary>
        private void StopLiftingObject()
        {
            isLiftingScale = 0;

            if (coupeledObject != null)
            {
                // Play the particle effect
                GameObject effectObject = ParticleUtil.CranePoof.SpawnParticle(coupeledObject.transform.position);

                // Remove the coupeled object
                Destroy(coupeledObject);
                coupeledObject = null;

                // Call the collect event
                craneTask.onIntelCollected.Invoke(1);
            }
        }

        /// <summary>
        /// Moves the crane claw using the isLiftingScale
        /// </summary>
        private void UpdateLiftingProces()
        {
            if (isLiftingScale != 0)
            {
                Vector3 armPosition = craneArm.position;

                //print("update");
                Vector3 pos = craneClaw.position;
                Vector3 endPosition = new Vector3(pos.x, armPosition.y + hookLiftStart, pos.z);

                if (isLiftingScale == 1)
                    endPosition = new Vector3(pos.x, armPosition.y + hookLiftEnd, pos.z);


                craneClaw.position = Vector3.Lerp(pos, endPosition, (hookLiftSpeed * Mathf.Abs(isLiftingScale)) * Time.deltaTime);

                float margin = clawLiftMargin;

                // When the claw reaches the start point when going up
                if (craneClaw.position.y >= armPosition.y + hookLiftStart - margin)
                {
                    StopLiftingObject();
                }
                // When the claw reaches the end point when going down
                else if (craneClaw.position.y <= armPosition.y + hookLiftEnd + margin)
                {
                    isLiftingScale = -1;
                }
            }
        }

        private void FixedUpdate()
        {
            UpdateLiftingProces();
        }

        /// <summary>
        /// This event is called when the CollisionEvent of the craneClaw is called
        /// </summary>
        /// <param name="_object"></param>
        private void OnClawCollisionEvent(GameObject _object)
        {
            CoupleObject(_object);
        }

        /// <summary>
        /// Sets the parent of the given object to that of the craneClaw transform
        /// </summary>
        /// <param name="_object"></param>
        /// <returns></returns>
        private bool CoupleObject(GameObject _object)
        {
            bool _return = false;

            if (coupeledObject == null)
            {
                coupeledObject = _object;

                _return = true;

                // Set the parent of the object to that of the Claw
                _object.transform.SetParent(craneClaw);

                // reset the local position
                _object.transform.localPosition = new Vector3();

                // Stop the movement of the object
                MoveInDirection _movingScript = _object.GetComponent<MoveInDirection>();

                if (_movingScript)
                    _movingScript.canMove = false;
            }

            return _return;
        }

        public override void MoveTarget(Vector3 location)
        {
        }

        public override void UseLeftAction(float pressedValue)
        {
        
        }

        public override void UseRightAction(float pressedValue)
        {
        }

#if UNITY_EDITOR

        /// <summary>
        /// Visual debug
        /// </summary>
        void OnDrawGizmos()
        {
            // Start position to calculate from
            Vector3 startPosition = originPositionTransform.position;

            Vector3 armPosition = craneArm.position;

            Vector3 clawPosition = craneClaw.position;

            Vector3 startPos = new Vector3(clawPosition.x, armPosition.y + hookLiftStart, clawPosition.z);
            Vector3 endPos = new Vector3(clawPosition.x, armPosition.y + hookLiftEnd, clawPosition.z);

            float _radius = 0.5f;

        
            // Start position
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(mousePosition, Vector3.up, 0.1f);

            // Start position
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(startPos, Vector3.up, _radius);

            // Up down range line
            UnityEditor.Handles.color = Color.blue;
            UnityEditor.Handles.DrawLine(startPos, endPos);

            // Claw position
            UnityEditor.Handles.color = Color.yellow;
            UnityEditor.Handles.DrawWireDisc(clawPosition, Vector3.up, _radius);

            // End position
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(endPos, Vector3.up, _radius);

            Quaternion rotation;
            Vector3 addDistanceToDirection;

            // Draw the max angle

            // local coordinate rotation around the Y axis to the given angle
            rotation = Quaternion.AngleAxis(maxAngle, Vector3.up);
            // add the desired distance to the direction
            addDistanceToDirection = rotation * transform.forward;

            UnityEditor.Handles.color = Color.yellow;
            UnityEditor.Handles.DrawLine(startPosition + (addDistanceToDirection * minRadius), startPosition + (addDistanceToDirection * maxRadius));
            UnityEditor.Handles.DrawLine(startPosition + (addDistanceToDirection * -minRadius), startPosition + (addDistanceToDirection * -maxRadius));

            // Draw the min/max radius

            // Draw minimum radius
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(startPosition, Vector3.up, minRadius);

            // Draw maximum radius
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(startPosition, Vector3.up, maxRadius);
        }

#endif
    }
}
