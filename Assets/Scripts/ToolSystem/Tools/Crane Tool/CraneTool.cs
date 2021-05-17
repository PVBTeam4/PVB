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

    /// <summary>
    /// Visual debug
    /// </summary>
    void OnDrawGizmos()
    {
        // Start position to calculate from
        Vector3 startPosition = originPositionTransform.position;

        Vector3 armPosition = craneArm.position;

        Vector3 clawPosition = craneClaw.position;

        Vector3 pos = new Vector3(clawPosition.x, armPosition.y - hookLiftStart, clawPosition.z);

        float _radius = 0.5f;

        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(pos, Vector3.up, _radius);

        UnityEditor.Handles.color = Color.blue;
        UnityEditor.Handles.DrawLine(pos, clawPosition);

        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(clawPosition, Vector3.up, _radius);

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

    private void CancelLifting()
    {

    }

    private void StartLiftingObject()
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
}
