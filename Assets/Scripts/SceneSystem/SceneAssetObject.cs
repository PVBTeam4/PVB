using UnityEngine;

namespace SceneSystem
{
    [CreateAssetMenu(fileName = "New SceneAssetObject", menuName = "SceneAssetObject")]
    public class SceneAssetObject : ScriptableObject
    {
        public string SceneName;
    }
}
