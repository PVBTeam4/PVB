using Input;
using System.Collections;
using System.Collections.Generic;
using ToolSystem;
using UnityEngine;


public class CraneTool : Tool
{
    [SerializeField]
    private Camera camera;

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

    private float isLiftingScale = 0;// 0 Is idle, 1 = moving down, -1 = moving up

    [Header("Mast rotation")]

    [SerializeField]
    private Transform originPositionTransform;

    [SerializeField]
    private Transform craneArm;

    [SerializeField]
    private Transform craneClaw;

    

    private void Start()
    {
        InputManager.MouseMovementAction += UpdateMousePosition;

        InputManager.ButtonInputAction += LeftAction;

        // Reset the crane claw position
        craneClaw.localPosition = craneClaw.localPosition.ChangeY(hookLiftStart);

        // Subscribe the OnClawCollisionEvent to the Collision event on the claw
        craneClaw.GetComponent<CraneClaw>().CollisionEvent += OnClawCollisionEvent;
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
        if (camera)
        {
            RaycastHit hit;
            //position.y -= -Screen.height / 2;
            //position.z = 5f;
            Ray ray = camera.ScreenPointToRay(position);
            // BUG: Look at canvas scale to solve it

            if (Physics.Raycast(ray, out hit))
            {
                if (originPositionTransform == null)
                    return;

                Vector3 originPos = originPositionTransform.position;
                originPos.y = 0;

                // Get the position of the hit poing
                Vector3 hitPosition = hit.point;

                // Set the position to the detected raycastpoint and clamp it
                Vector3 pos = hitPosition.ClampMagnitudeMinMax(maxRadius, minRadius);
                pos.y = 0;

                transform.position = pos + offset;
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
        isLiftingScale = -1;
        print("stop");
    }

    private void UpdateLiftingProces()
    {
        if (isLiftingScale != 0)
        {
            //print("update");
            Vector3 pos = craneClaw.localPosition;
            Vector3 endPosition = new Vector3(pos.x, hookLiftStart, pos.z);

            if (isLiftingScale == 1)
            endPosition = new Vector3(pos.x, hookLiftEnd, pos.z);


            craneClaw.localPosition = Vector3.Lerp(pos, endPosition, (hookLiftSpeed * Mathf.Abs(isLiftingScale)) * Time.deltaTime);

            float margin = 0.1f;

            if (craneClaw.localPosition.y >= hookLiftStart - margin)
            {
                isLiftingScale = 0;
            }
            else if (craneClaw.localPosition.y <= hookLiftEnd + margin)
            {
                isLiftingScale = -1;
                print(endPosition);
            }

            //Debug.Log(craneClaw.localPosition.y);

            //craneClaw.position = craneClaw.position.ClampY(craneArm.position.y + hookLiftStart, craneArm.position.y + hookLiftEnd);
        }
    }

    private void Update()
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

        Vector3 startPos = new Vector3(clawPosition.x, armPosition.y - hookLiftStart, clawPosition.z);
        Vector3 endPos = new Vector3(clawPosition.x, armPosition.y + hookLiftEnd, clawPosition.z);

        float _radius = 0.5f;

        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(startPos, Vector3.up, _radius);

        // Up down range line
        UnityEditor.Handles.color = Color.blue;
        UnityEditor.Handles.DrawLine(startPos, endPos);

        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(clawPosition, Vector3.up, _radius);

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
