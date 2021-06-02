using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Input;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CraneBehaviour : MonoBehaviour
{
    [Header("Crane Transforms")]

    [SerializeField]
    private GameObject mainCraneObject;

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

    [SerializeField]
    private GameObject[] craneUIObjects;

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

    private float previousInteractionButtonState;

    private bool craneActive;

    public bool CraneActive { get => craneActive;
        set
        {
            craneActive = value;

            ToggleObjects(craneUIObjects, craneActive);

            CraneActivationEvent.Invoke(value);
        }
    }

    // Event for when the crane activates
    private Action<bool> craneActivationEvent;
    public Action<bool> CraneActivationEvent { get => craneActivationEvent; set => craneActivationEvent = value; }

    // The distance the crane needs to be from the mainCraneObject when its not active
    [SerializeField]
    private float craneNonActiveDistance = 2.35f;

    private void Start()
    {
        // Deactivate Crane
        CraneActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Subscribe the crane activation
        InputManager.ButtonInputAction += CraneActivation;

        ScaleCableToClaw();

        Vector3 targetTransformPosition;

        if (craneActive)
        {
            // Get the targetTransformPosition from the targetTransform
            targetTransformPosition = targetTransform.position;
        }
        else
        {
            Vector3 position = mainCraneObject.transform.forward * craneNonActiveDistance;
            position += mainCraneObject.transform.position;

            targetTransformPosition = position;
        }

        // Rotate the craneMast transform towards the targetTransformPosition using the RotateMastTowardsMouse function
        RotateMastTowardsPoint(targetTransformPosition);

        // Move the craneHook transform towards the targetTransformPosition along its Z axis using the MoveCraneHookAlongArm function
        MoveCraneHookAlongArm(targetTransformPosition);
    }

    /// <summary>
    /// Rotates the crane towards the mouse position, ignoring the X rotation
    /// </summary>
    /// <param name="pointPosition"></param>
    private void RotateMastTowardsPoint(Vector3 pointPosition)
    {
        // Get the relative position of the Point & Crane
        Vector3 relativePos = pointPosition - craneMast.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotationTowardsPoint = Quaternion.LookRotation(relativePos, Vector3.up);

        // Ignore the X & Z rotation
        rotationTowardsPoint.x = 0;
        rotationTowardsPoint.z = 0;

        Quaternion rotation = Quaternion.Slerp(craneMast.rotation, rotationTowardsPoint, craneRotationSpeed * Time.deltaTime);

        // Set the rotation of the Crane to that of the new one towards the mouse
        craneMast.rotation = rotation;
    }

    /// <summary>
    ///  Moves crane hook towards the mouse position along the arm
    /// </summary>
    /// <param name="pointPosition"></param>
    private void MoveCraneHookAlongArm(Vector3 pointPosition)
    {
        // Lerp the given Transform towards the given target Transform
        float step = craneHookFollowSpeed * Time.deltaTime;// Calculate distance to move
        craneHook.position = Vector3.Lerp(craneHook.position, pointPosition, step);

        Vector3 localPosition = craneHook.localPosition;
        craneHook.localPosition = new Vector3(0, 0, localPosition.z);
    }

    // Scales the given cable towards the given claw. Using the distance between their positions
    private void ScaleCableToClaw()
    {
        Vector3 cableStartPosition = craneCable.position;
        Vector3 clawPosition = craneClaw.position;

        float distance = Vector3.Distance(cableStartPosition, clawPosition);

        craneCable.localScale = craneCable.localScale.ChangeY(distance);
    }

    /// <summary>
    /// Will toggle the crane via the InputManager Interaction button
    /// </summary>
    /// <param name="button"></param>
    /// <param name="amount"></param>
    private void CraneActivation(ButtonInputType button, float amount)
    {
        // Only run the code once when the button is pressed
        if (button == ButtonInputType.Interaction && amount != previousInteractionButtonState)
        {
            previousInteractionButtonState = amount;

            if (amount > 0)
            {
                ToggleCrane();
            }
        }
    }

    /// <summary>
    /// Toggles the crane
    /// </summary>
    private void ToggleCrane()
    {
        if (mainCraneObject)
        {
            CraneActive = !CraneActive;
        }
        else
            Debug.LogError("No mainCraneObject found");
    }

    /// <summary>
    /// Enables or Disables all the objects in the list depending on the given state
    /// </summary>
    /// <param name="gameObjects"></param>
    /// <param name="state"></param>
    private void ToggleObjects(GameObject[] gameObjects, bool state)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(state);
        }
    }

#if UNITY_EDITOR


    /// <summary>
    /// Visual debug
    /// </summary>
    void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.red;

        Vector3 position = mainCraneObject.transform.forward * 2;
        position += mainCraneObject.transform.position;

        UnityEditor.Handles.DrawWireCube(position, new Vector3(1, 1, 1));

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

#endif
}
