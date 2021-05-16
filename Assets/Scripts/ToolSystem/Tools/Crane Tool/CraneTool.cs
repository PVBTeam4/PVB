using Input;
using System.Collections;
using System.Collections.Generic;
using ToolSystem;
using UnityEngine;


public class CraneTool : Tool
{

    float liftingProcess, radius = 10;
    //LiftObjective currentLifting;

    [SerializeField]
    private Transform originPositionTransform;

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
        RaycastHit hit;
        //position.y -= -Screen.height / 2;
        //position.z = 5f;
        Ray ray = Camera.current.ScreenPointToRay(position);
        print(ray);
        // BUG: Look at canvas scale to solve it

        if (Physics.Raycast(ray, out hit))
        {
            if (originPositionTransform == null)
                return;

            Vector3 originPos = originPositionTransform.position;
            originPos.y = 0;

            //Set the position to the detected raycastpoint
            Vector3 pos = hit.point - originPos;
            pos.y = 0;

            pos = Vector3.ClampMagnitude(pos, radius);

            transform.position = pos + originPos;

            // Do something with the object that was hit by the raycast.
        }
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
