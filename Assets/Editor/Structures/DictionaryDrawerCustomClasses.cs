using Editor.Structures.SerializedDictionary;
using Global;
using SceneSystem;
using UnityEditor;

namespace Editor.Structures
{
    /// <summary>
    /// Adds the abillity to draw the SceneDictionary in the Unity Editor
    /// </summary>
    [CustomPropertyDrawer(typeof(SceneDictionary))]
    public class SceneDictionaryDrawer : DictionaryDrawer<ToolType, SceneAssetObject> { }
}