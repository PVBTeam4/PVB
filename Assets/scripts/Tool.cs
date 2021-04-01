using System.Collections;
using System.Collections.Generic;
using Global;
using UnityEngine;

/// <summary>
/// Abstract class for every tool object to inherit from. Every Tool class is expected to own the properties written below.
/// </summary>
public abstract class Tool : MonoBehaviour
{
    //Enum property to describe the type of the tool
    private ToolType toolType { get; }

    /// <summary>
    /// Abstract method to describe what happens when the left mouse button is pressed
    /// </summary>
    public abstract void UseLeftAction();

    /// <summary>
    /// Abstract method to describe what happens when the right mouse button is pressed
    /// </summary>
    public abstract void UseRightAction();

    /// <summary>
    /// Abstract method to describe what happens when the mouse is moved around
    /// </summary>
    public abstract void MoveTarget(Vector3 location);
}
