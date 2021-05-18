using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CraneBehaviour : MonoBehaviour
{
    [Header("Crane Transforms")]

    [SerializeField]
    private Transform craneMast;

    [SerializeField]
    private Transform craneArm;

    [SerializeField]
    private Transform craneHook;

    [SerializeField]
    private Transform craneCable;

    [SerializeField]
    private Transform craneClaw;

    [Header("Crane Variables")]

    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private float craneAngleMax = 45;

    [SerializeField]
    private float craneRotationSpeed = 5;

    [Header("Hook Variables")]

    [SerializeField]
    private float craneHookFollowSpeed = 0.75f;

    [SerializeField]
    private float minRadius = 2.5f;

    [SerializeField]
    private float maxRadius = 7.5f;

    // Update is called once per frame
    void Update()
    {
        // Get the targetTransformPosition from the targetTransform
        Vector3 targetTransformPosition = targetTransform.position;

        // Rotate the craneMast transform towards the targetTransformPosition using the RotateMastTowardsMouse function
        RotateMastTowardsMouse(targetTransformPosition);

        // Move the craneHook transform towards the targetTransformPosition along its Z axis using the MoveCraneHookAlongArm function
        MoveCraneHookAlongArm(targetTransformPosition);

        ScaleCableToClaw();
    }

    // Rotates the crane towards the mouse position, ignoring the X rotation
    private void RotateMastTowardsMouse(Vector3 mousePosition) 
    {
        // Get the relative position of the Mouse & Crane
        Vector3 relativePos = mousePosition - craneMast.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotationTowardsMouse = Quaternion.LookRotation(relativePos, Vector3.up);
        
        // Ignore the X rotation
        rotationTowardsMouse.x = 0;
        rotationTowardsMouse.z = 0;

        // Clamp the rotation
        //rotationTowardsMouse.y = Mathf.Clamp(rotationTowardsMouse.y, 0, 10);

        Quaternion rotation = Quaternion.Slerp(craneMast.rotation, rotationTowardsMouse, craneRotationSpeed * Time.deltaTime);

        // Set the rotation of the Crane to that of the new one towards the mouse
        craneMast.rotation = rotation;
    }

    // Moves crane hook towards the mouse position along the 
    private void MoveCraneHookAlongArm(Vector3 mousePosition)
    {
        // Lerp the given Transform towards the given target Transform
        float step = craneHookFollowSpeed * Time.deltaTime;// Calculate distance to move
        craneHook.position = Vector3.Lerp(craneHook.position, targetTransform.position, step);

        Vector3 localPosition = craneHook.localPosition;
        craneHook.localPosition = new Vector3(0, 0, localPosition.z);
    }

    // Scales the given cable towards the given claw. Using the distance between their positions
    private void ScaleCableToClaw()
    {
        Vector3 cableStartPosition = craneCable.position;
        Vector3 clawPosition = craneClaw.position;

        float distance = Vector3.Distance(cableStartPosition, clawPosition);

        //print("distance: " + distance);

        craneCable.localScale = craneCable.localScale.ChangeY(distance);

    }

    /// <summary>
    /// Visual debug
    /// </summary>
    void OnDrawGizmos()
    {
        // Start position to calculate from
        Vector3 startPosition = craneMast.position;

        Quaternion rotation;
        Vector3 addDistanceToDirection;

        // Draw the max angle

        // local coordinate rotation around the Y axis to the given angle
        rotation = Quaternion.AngleAxis(craneAngleMax, Vector3.up);
        // add the desired distance to the direction
        addDistanceToDirection = rotation * craneMast.forward;

        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawLine(startPosition + (addDistanceToDirection * minRadius), startPosition + (addDistanceToDirection * maxRadius));
        UnityEditor.Handles.DrawLine(startPosition + (addDistanceToDirection * -minRadius), startPosition + (addDistanceToDirection * -maxRadius));
    }
}

