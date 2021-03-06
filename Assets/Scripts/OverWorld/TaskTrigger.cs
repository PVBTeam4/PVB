using System;
using Global;
using SceneSystem;
using UnityEngine;

namespace OverWorld
{
    /// <summary>
    /// Script that utilizes an 'OnTriggerEnter' event to switch to a given scene based on a given playername. This is used on colliders/gameobjects 
    /// that each correspond with a certain scene.
    /// </summary>
    public class TaskTrigger : MonoBehaviour
    {
        // Name of player gameObject's tag
        private const string PlayerTag = "Player";

        public Action<ToolType> SceneSwitchEvent;

        //ToolType, to load correct scene
        [SerializeField]
        private ToolType toolType;

        private void Awake()
        {
            Collider collider = GetComponent<Collider>();
            if (collider == null || !collider.isTrigger)
            {
                Debug.LogError(gameObject.name + " does not have a trigger.");
            }

            // Subscribe the overworldTasks.CompleteTaskEvent to the SceneSwitchEvent
            OverworldTasks overworldTasks = FindObjectOfType<OverworldTasks>();
            if (overworldTasks) SceneSwitchEvent += (ToolType _type) => { overworldTasks.CompleteTaskEvent(2); };
        }

        /// <summary>
        /// 'OnTriggerEnter' event to detect the player colliding with this object
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(PlayerTag))
            {
                SceneController.SwitchSceneByToolType(toolType);

                SceneSwitchEvent(toolType);

                gameObject.SetActive(false);
            }
        }
    }
}
