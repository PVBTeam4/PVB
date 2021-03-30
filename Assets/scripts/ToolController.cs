using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController
{
    private Tool activeTool;
    private Dictionary<ToolType, Tool> tools; //<-- supposed to be defined by gamemanager

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

    public void UseToolLeftAction(){
        activeTool.UseLeftAction();
    }

    public void UseToolRightAction(){
        activeTool.UseRightAction();
    }

    public void UseToolMouseTarget(){
        //activeTool.MoveTarget(/*mouse position*/);
    }

    public void SetActiveTool(ToolType toolType){
        activeTool.toolType = toolType;
    }
}
