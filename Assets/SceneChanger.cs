using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneSystem;
using Global;

public class SceneChanger : MonoBehaviour
{

    public void Scene(string name)
    {
        Time.timeScale = 0;
        SceneController.SwitchScene(name);
    }
    public void SceneByCannon()
    {
        Time.timeScale = 0;
        SceneController.SwitchSceneByToolType(ToolType.CANNON);
    }
}
