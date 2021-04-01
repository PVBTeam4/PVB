using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager;
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
        private static SceneController _instance;

        [SerializeField]
        // The main scene where the game plays
        private SceneAsset overWorldScene;

        [SerializeField]
        // Used to link a Scene to their respective Type 
        private SceneDictionary sceneDictionary;

        // Returns the instance of the SceneDictionary
        public SceneDictionary SceneDictionaryInstance { get => sceneDictionary; }
        
        // Fire this event when you want to switch to the Over World Scene
        public static Action OverWorldEnterAction;

        // Fire this event when you want to switch to a Tool Scene
        public static Action<ToolType> TaskModeEnterAction;

        /// <summary>
        /// This runs before the Start method
        /// </summary>
        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnEnable()
        {
            _instance = this;
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
        /// Loads the scene that is put on "overWorldScene" 
        /// </summary>
        public static void SwitchSceneToOverWorld()
        {
            SceneController sceneController = _instance;
            if (sceneController == null)
            {
                Debug.LogError("SceneController is not yet loaded.");
                return;
            }
            // Check if the Scene exists
            if (sceneController.overWorldScene)
            {
                // Load the Scene using the Name of the SceneAsset
                SceneManager.LoadScene(sceneController.overWorldScene.name);
            }
            else// Give an Error if the overWorldScene is not found
            {
                Debug.LogError("OverWorld Scene is not found");
            }
        }

        /// <summary>
        /// Switches to the Scene retrieved from the SceneDictionary using the ToolType
        /// </summary>
        /// <param name="toolType">Type of Scene that needs to be loaded in</param>
        public static void SwitchSceneByToolType(ToolType toolType)
        {
            SceneController sceneController = _instance;
            if (sceneController == null)
            {
                Debug.LogError("SceneController is not yet loaded.");
                return;
            }
            
            // Try and get the right Scene from the SceneDictionary
            if (!sceneController.sceneDictionary.TryGetValue(toolType, out SceneAsset scene))
            {
                // Give an Error if the ToolType does not have a Scene
                Debug.LogError("Scene not found for ToolType: " + toolType);
                return;
            }
            
            // Load the Scene using the Name of the SceneAsset
            SceneManager.LoadScene(scene.name);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            ToolType toolType = GetToolTypeByScene(scene);

            if (toolType == ToolType.NOONE)
            {
                if (scene.name == overWorldScene.name)
                {
                    OverWorldEnterAction?.Invoke();
                }
                return;
            }
            
            TaskModeEnterAction?.Invoke(toolType);
        }

        private ToolType GetToolTypeByScene(Scene scene)
        {
            try
            {
                return sceneDictionary.First(toolTypeScenePair => toolTypeScenePair.Value.name == scene.name).Key;
            }
            catch (InvalidOperationException)
            {
                return ToolType.NOONE;
            }
        }
    }
}



