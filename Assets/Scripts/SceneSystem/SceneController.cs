using System;
using System.Linq;
using Global;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace SceneSystem
{

    /// <summary>
    /// Holds functions to load scenes via the ToolType enum
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        // Instance of this class
        private static SceneController _instance;

        [SerializeField]
        // The main scene where the game plays
        private SceneAssetObject[] overworldSceneArray;

        [SerializeField]
        // Used to link a Scene to their respective Type 
        private SceneAssetObject[] taskSceneArray;

        // Fire this event when you want to switch to the Over World Scene
        [SerializeField] private UnityEvent OverWorldEnterAction;

        // Fire this event when you want to switch to a Tool Scene
        [SerializeField] private UnityEvent<ToolType> TaskModeEnterAction;

        /// <summary>
        /// This runs before the Start method
        /// </summary>
        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        /// <summary>
        /// On GameObject Enabled
        /// </summary>
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
        /// Loads a new overworld scene based on the tooltype of the last task
        /// </summary>
        /// <param name="toolType"></param>
        public static void SwitchSceneToOverWorld(ToolType toolType)
        {
            SceneController sceneController = _instance;
            if (sceneController == null)
            {
                Debug.LogError("SceneController is not yet loaded.");
                return;
            }

            switch(toolType)
            {
                case ToolType.CANNON:
                    // Gives an Error if the ToolType does not have a corresponding overworld scene
                    //SceneAssetObject scene = sceneController.overworldSceneArray[overworldIndex];
                    if (!sceneController.overworldSceneArray[1])
                    {
                        Debug.LogError("Overworld scene not found for ToolType: " + ToolType.CANNON);
                        return;
                    }
                    SceneManager.LoadScene(sceneController.overworldSceneArray[1].name);
                    break;
                case ToolType.CRANE:
                    if (!sceneController.overworldSceneArray[2])
                    {
                        Debug.LogError("Overworld scene not found for ToolType: " + ToolType.CRANE);
                        return;
                    }
                    SceneManager.LoadScene(sceneController.overworldSceneArray[2].name);
                    break;
                case ToolType.WATER_CANNON:
                    Debug.Log("End of game");
                    return;
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
            if (!sceneController.TryGetSceneAssetByToolType(toolType, out SceneAssetObject scene))
            {
                // Give an Error if the ToolType does not have a Scene
                Debug.LogError("Scene not found for ToolType: " + toolType);
                return;
            }
            
            // Load the Scene using the Name of the SceneAsset
            SceneManager.LoadScene(scene.SceneName);
        }

        /// <summary>
        /// This funtion will be run when the scene has been loaded
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="loadSceneMode"></param>
        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            // Get the ToolType of the given Scene
            ToolType toolType = GetToolTypeByScene(scene);

            // Check if the OverWorld needs to be loaded
            if (toolType == ToolType.NOONE)
            {
                if (overworldSceneArray[0])
                {
                    // Fire the event that the player wants to enter the overworld
                    OverWorldEnterAction?.Invoke();
                }
                return;
            }
            
            // Fire the event that a task-mode has been started
            TaskModeEnterAction?.Invoke(toolType);
        }

        /// <summary>
        /// Tries to return the ToolType of the given Scene
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        private ToolType GetToolTypeByScene(Scene scene)
        {
            // Try to compare the given Scene to the sceneDictionary what its ToolType is
            try
            {
                return taskSceneArray.First(sceneAssetObject => sceneAssetObject.SceneName == scene.name).ToolType;
            }
            catch (InvalidOperationException)// If not found. Just return the default value
            {
                return ToolType.NOONE;
            }
        }

        private bool TryGetSceneAssetByToolType(ToolType toolType, out SceneAssetObject sceneAssetObject)
        {
            sceneAssetObject = taskSceneArray.FirstOrDefault(scene => scene.ToolType == toolType);
            Debug.Log(sceneAssetObject);
            return sceneAssetObject != null;
        }
    }
}



