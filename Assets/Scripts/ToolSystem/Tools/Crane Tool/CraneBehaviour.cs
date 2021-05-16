using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform craneMast;

    [SerializeField]
    private Transform craneArm;

    [SerializeField]
    private Transform craneHook;

    [SerializeField]
    private Transform targetTransform;

    [Header("Hook Variables")]

    [SerializeField]
    private float craneHookFollowSpeed = 0.5f;

    [SerializeField]
    private float minRadius = 0.5f;

    [SerializeField]
    private float maxRadius = 3;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the targetTransformPosition from the targetTransform
        Vector3 targetTransformPosition = targetTransform.position;

        // Rotate the craneMast transform towards the targetTransformPosition using the RotateMastTowardsMouse function
        RotateMastTowardsMouse(targetTransformPosition);

        // Move the craneHook transform towards the targetTransformPosition along its Z axis using the MoveCraneHookAlongArm function
        MoveCraneHookAlongArm(targetTransformPosition);
    }

    // Rotates the crane towards the mouse position, ignoring the X rotation
    private void RotateMastTowardsMouse(Vector3 mousePosition) 
    {
        // Get the relative position of the Mouse & Crane
        Vector3 relativePos = mousePosition - transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotationTowardsMouse = Quaternion.LookRotation(relativePos, Vector3.up);
        
        // Ignore the X rotation
        rotationTowardsMouse.x = 0;
        rotationTowardsMouse.z = 0;

        // Set the rotation of the Crane to that of the new one towards the mouse
        craneMast.rotation = rotationTowardsMouse;
    }

    // Moves crane hook towards the mouse position along the 
    private void MoveCraneHookAlongArm(Vector3 mousePosition)
    {
        // Lerp the given Transform towards the given target Transform
        craneHook.position = Vector3.Lerp(craneHook.position, targetTransform.position, craneHookFollowSpeed * Time.deltaTime);

        Vector3 localPosition = craneHook.localPosition;
        craneHook.localPosition = new Vector3(0, 0, localPosition.z);


        // Clamp the Hook along the arm via between the min and max radius
        //z = clamp(z, minRadius, maxRadius);
    }
}
