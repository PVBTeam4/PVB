using Global;
using UnityEngine;

namespace SceneSystem
{
    /// <summary>
    /// Object used to hold Scene Info
    /// </summary>
    [CreateAssetMenu(fileName = "New SceneAssetObject", menuName = "SceneAssetObject")]
    public class SceneAssetObject : ScriptableObject
    {
        // Scene name
        [SerializeField]
        private string sceneName;
        
        // ToolType of scene
        [SerializeField]
        private ToolType toolType;
        
        public string SceneName
        {
            get => sceneName;
            private set => sceneName = value;
        }
        
        public ToolType ToolType
        {
            get => toolType;
            private set { this.toolType = value; }
        }
    }
}
