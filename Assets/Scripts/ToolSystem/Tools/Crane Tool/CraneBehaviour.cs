using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneBehaviour : MonoBehaviour
{
    [SerializedField]
    private Transform craneMast;

    [SerializedField]
    private Transform craneArm;

    [SerializedField]
    private Transform craneHook;

    [SerializedField]
    private Transform mouseTarget;

    [SerializedField]
    private float minRadius = 0.5;

    [SerializedField]
    private float maxRadius = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouseTargetPosition from the mouseTarget
        Vector3 mouseTargetPosition = mouseTarget.position;

        // Rotate the craneMast transform towards the mouseTargetPosition using the RotateMastTowardsMouse function
        RotateMastTowardsMouse(mouseTargetPosition);

        // Move the craneHook transform towards the mouseTargetPosition along its Z axis using the MoveCraneHookAlongArm function
        MoveCraneHookAlongArm(mouseTargetPosition);
    }

    // Rotates the crane towards the mouse position, ignoring the X rotation
    RotateMastTowardsMouse(Vector3 mousePosition) 
    {
        // Get the relative position of the Mouse & Crane
        Vector3 relativePos = mousePosition - transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotationTowardsMouse = Quaternion.LookRotation(relativePos, Vector3.up);
        
        // Ignore the X rotation
        rotationTowardsMouse.x = 0;

        // Set the rotation of the Crane to that of the new one towards the mouse
        craneMast.rotation = rotationTowardsMouse;
    }

    // Moves crane hook towards the mouse position along the 
    MoveCraneHookAlongArm(Vector3 mousePosition)
    {
        // Clamp the Hook along the arm via between the min and max radius
        //z = clamp(z, minRadius, maxRadius);
    }
}
