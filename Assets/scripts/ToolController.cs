using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the tools/tasks on a higher level of abstraction
/// </summary>
public class ToolController
{
    //The tool that's currently being focused on
    private Tool activeTool;

    //Dictionary of tools that can be managed using their type as Keys
    private Dictionary<ToolType, Tool> tools;

    /// <summary>
    /// OnInput event that when fired will initiate some action of the current tool of focus
    /// </summary>
    public void OnInput(InputType inputType){
        switch (inputType)
        {
            case InputType.LEFT_MOUSE:
                UseToolLeftAction();
                break;
            case InputType.RIGHT_MOUSE:
                UseToolRightAction();
                break;
            case InputType.MOUSE_MOVE:
                UseToolMouseTarget();
                break;
        }
    }

    /// <summary>
    /// Carries out the action assigned to the left mouse button
    /// </summary>
    public void UseToolLeftAction(){
        activeTool.UseLeftAction();
    }

    /// <summary>
    /// Carries out the action assigned to the right mouse button
    /// </summary>
    public void UseToolRightAction(){
        activeTool.UseRightAction();
    }

    /// <summary>
    /// Carries out the action assigned to the moving around of the mouse
    /// </summary>
    public void UseToolMouseTarget(){
        //activeTool.MoveTarget(/*mouse position*/);
    }

    /// <summary>
    /// Sets the currently active tool to one of a different type
    /// </summary>
    public void SetActiveTool(ToolType toolType){
        Tool foundTool;
        if(tools.TryGetValue(toolType, out foundTool))
        {
            activeTool = foundTool;
        }
        else
        {
            Debug.LogError("tool not registered");
        }
    }
}
