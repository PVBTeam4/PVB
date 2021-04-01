using System.Data;
using Global;
using Input;
using Input.AbstractListeners;
using UnityEngine;

/// <summary>
/// Manages the tools/tasks on a higher level of abstraction
/// </summary>
public class ToolController : ButtonAndMouseMovementInputListener
{
    //The tool that's currently being focused on
    private readonly Tool _activeTool;

    public ToolController(ToolType activeToolType)
    {
        _activeTool = GetToolByType(activeToolType);
        RegisterInput();
    }

    public void DeConstruct()
    {
        DeRegisterInput();
    }

    /// <summary>
    /// OnInputReceived event that when fired will initiate some action of the current tool of focus
    /// </summary>
    protected override void OnInputReceived(ButtonInputType buttonInputType, float value)
    {
        switch (buttonInputType)
        {
            case ButtonInputType.LeftMouse:
                UseToolLeftAction();
                break;
            case ButtonInputType.RightMouse:
                UseToolRightAction();
                break;
        }
    }

    /// <summary>
    /// Carries out the action assigned to the moving around of the mouse
    /// </summary>
    protected override void OnMouseMovementReceived(Vector3 mousePosition)
    {
        if (_activeTool == null) return;
        _activeTool.MoveTarget(mousePosition);
    }

    /// <summary>
    /// Carries out the action assigned to the left mouse button
    /// </summary>
    private void UseToolLeftAction(){
        _activeTool.UseLeftAction();
    }

    /// <summary>
    /// Carries out the action assigned to the right mouse button
    /// </summary>
    private void UseToolRightAction(){
        _activeTool.UseRightAction();
    }

    private Tool GetToolByType(ToolType toolType)
    {
        switch (toolType)
        {
            case ToolType.CANNON:
                return Object.FindObjectOfType<CannonTool>();
        }
        throw new SyntaxErrorException("Tool could not be found!");
    }
}
