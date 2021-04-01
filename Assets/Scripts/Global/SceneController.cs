using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    /// <summary>
    /// This Dictionary Class (extended from SerializableDictionary) is used to link Scenes to the ToolType enum
    /// </summary>
    [Serializable] public class SceneDictionary : SerializableDictionary<ToolType, SceneAsset> { }

    /// <summary>
    /// Holds functions to load scenes via the ToolType enum
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        [SerializeField]
        // The main scene where the game plays
        private SceneAsset overworldScene;

        [SerializeField]
        // Used to link a Scene to their respective Type 
        private SceneDictionary sceneDictionary;

        // Returns the instance of the SceneDictionary
        public SceneDictionary SceneDictionaryInstance { get => sceneDictionary; }

        /// <summary>
        /// This runs before the Start method
        /// </summary>
        private void Awake()
        {
            // Assign all the Action Events
            AssignAllActionEvents();
        }

        /// <summary>
        /// Here we assign all the Action Events 
        /// </summary>
        private void AssignAllActionEvents()
        {
            // Add the SwitchSceneByToolType function to the GameManager.SwitchedSceneByToolType Action Event
            GameManager.SwitchedSceneByToolType += SwitchSceneByToolType;

            // Add the SwitchSceneToOverworld function to the GameManager.SwitchedSceneByToolType Action Event
            GameManager.SwitchedSceneToOverworld += SwitchSceneToOverworld;
        }

        /// <summary>
        /// Here we remove all the Action Events. This for cleanup reasons.
        /// </summary>
        private void RemoveAllActionEvents()
        {
            // Remove the SwitchSceneByToolType function to the GameManager.SwitchedSceneByToolType Action Event
            GameManager.SwitchedSceneByToolType -= SwitchSceneByToolType;

            // Remove the SwitchSceneToOverworld function to the GameManager.SwitchedSceneByToolType Action Event
            GameManager.SwitchedSceneToOverworld -= SwitchSceneToOverworld;
        }

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
        /// Loads the scene that is put on "overworldScene" 
        /// </summary>
        public void SwitchSceneToOverworld()
        {
            // Check if the Scene exists
            if (overworldScene)
            {
                // Load the Scene using the Name of the SceneAsset
                SceneManager.LoadScene(overworldScene.name);
            }
            else// Give an Error if the overworldScene is not found
            {
                Debug.LogError("Overworld Scene is not found");
            }
        }

        /// <summary>
        /// Switches to the Scene retreived from the SceneDictionary using the ToolType
        /// </summary>
        /// <param name="toolType">Type of Scene that needs to be loaded in</param>
        public void SwitchSceneByToolType(ToolType toolType)
        {
            // Try and get the right Scene from the SceneDictionary
            SceneAsset scene;
            this.sceneDictionary.TryGetValue(toolType, out scene);
            
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



