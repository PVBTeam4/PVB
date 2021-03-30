using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    /// <summary>
    /// This Dictionary is used to link Scenes to the ToolType enum
    /// </summary>
    [Serializable] public class SceneDictionary : SerializableDictionary<ToolType, SceneAsset> { }

    /// <summary>
    /// Holds functions to load scenes via the ToolType enum
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        [SerializeField]
        // Used to link a Scene to their respective Type 
        private SceneDictionary sceneDictionary;

        // Returns the instance of the SceneDictionary
        public SceneDictionary SceneDictionaryInstance { get => sceneDictionary; }

        /// <summary>
        /// Switches to the given scene
        /// </summary>
        /// <param name="sceneName">Name of the scene that needs to be loaded in</param>
        public void SwitchScene(string sceneName)
        {
            // Check if the Scene exists
            if (SceneManager.GetSceneByName(sceneName) != null)
            {
                SceneManager.LoadScene(sceneName);
            }
            else// Give an Error if the ToolType does not have a Scene
            {
                Debug.LogError("Scene not with name: " + sceneName);
            }
        }

        /// <summary>
        /// Switches to the Scene retreived from the SceneDictionary using the ToolType
        /// </summary>
        /// <param name="toolType">Type of Scene that needs to be loaded in</param>
        /// <param name="sceneDictionary">The SerializableDictionary that holds the Scenes</param>
        public void SwitchSceneByToolType(ToolType toolType, SceneDictionary sceneDictionary)
        {
            // Try and get the right Scene from the SceneDictionary
            SceneAsset scene;
            sceneDictionary.TryGetValue(toolType, out scene);
            
            // Check if the Scene exists
            if (scene)
            {
                // Load the Scene using the Name of the SceneAsset
                SceneManager.LoadScene(scene.name);
            }
            else// Give an Error if the ToolType does not have a Scene
            {
                Debug.LogError("Scene not found for ToolType: " + toolType.ToString());
            }
        }
    }
}



