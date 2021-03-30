using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable] public class SceneDictionary : SerializableDictionary<ToolType, SceneAsset> { }

public class SceneController : MonoBehaviour
{
    [SerializeField]
    // Used to link a Scene to their respective Type 
    private SceneDictionary sceneDictionary;

    // Returns the instance of the SceneDictionary
    public SceneDictionary SceneDictionaryInstance { get => sceneDictionary; }

    // Switches to the given scene
    public void SwitchScene(ToolType toolType, SceneDictionary sceneDictionary)
    {
        // Get the right Scene from the SceneDictionary
        SceneAsset scene = sceneDictionary[toolType];

        // Check if the Scene exists
        if (scene)
        {
            SceneManager.LoadScene(scene.name);
        }
        else// Give an Error if the ToolType does not have a Scene
        {
            Debug.LogError("Scene not found for ToolType: " + toolType.ToString());
        }
        
    }
}


