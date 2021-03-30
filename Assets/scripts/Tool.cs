using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    public ToolType toolType;

    public abstract void UseLeftAction();

    public abstract void UseRightAction();

    public abstract void MoveTarget(Vector3 location);
}