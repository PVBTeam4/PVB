using Input;
using System.Collections;
using System.Collections.Generic;
using ToolSystem;
using UnityEngine;


public class CraneTool : Tool
{
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
    private float clawLiftMargin = 0.08f;// How soon before it returns

    private float isLiftingScale = 0;// 0 Is idle, 1 = moving down, -1 = moving up

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

        // Get the crane task
        craneTask = FindObjectOfType<CraneTask>();

        if (!craneTask)
            Debug.Log("No object found with the CraneTask script. Try adding the 'CraneTaskManager' prefab");
    }

    private void UpdateMousePosition(Vector3 position)
    {
        HandleMovement(position);
    }

    private void HandleLifting()
    {

    }

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
                if (originPositionTransform == null)
                    return;

                mousePosition = hit.point;

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
        }
        else
        {
            Debug.LogError("No Camera found");
        }
    }

    private void LeftAction(ButtonInputType type, float amount)
    {
        if (amount > 0)
        {
            if (type.Equals(ButtonInputType.LeftMouse))
            {
                if (isLiftingScale == 0)
                    StartLiftingObject();
            }       
        }
    }

    private void StartLiftingObject()
    {
        isLiftingScale = 1;
        print("start");
    }

    private void StopLiftingObject()
    {
        isLiftingScale = 0;

        if (coupeledObject != null)
        {
            Destroy(coupeledObject);

            coupeledObject = null;

            // Call the collect event
            craneTask.onIntelCollected.Invoke(1);
        }

        print("stop");
    }

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

            if (craneClaw.position.y >= armPosition.y + hookLiftStart - margin)
            {
                StopLiftingObject();
            }
            else if (craneClaw.position.y <= armPosition.y + hookLiftEnd + margin)
            {
                isLiftingScale = -1;
            }

            //Debug.Log(craneClaw.localPosition.y);

            //craneClaw.position = craneClaw.position.ClampY(craneArm.position.y + hookLiftStart, craneArm.position.y + hookLiftEnd);
        }
    }

    private void FixedUpdate()
    {
        UpdateLiftingProces();
    }

    private void OnClawCollisionEvent(GameObject _object)
    {
        CoupleObject(_object);
    }

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

            // Disable the water script
            //MoveInDirection

            if (_movingScript)
                _movingScript.canMove = false;

            Debug.LogWarning("coupeling object");
        }
        else
        {

        }

        return _return;
    }

    private void CancelLifting()
    {

    }

    private void GetLiftObjective()
    {

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
}
