using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// This is the place to add custom SerializableDictionaries that will give the Unity Editor the abillity to draw these Dictionaries
/// </summary>
namespace Global
{
    /// <summary>
    /// Adds the abillity to draw the SceneDictionary in the Unity Editor
    /// </summary>
    [CustomPropertyDrawer(typeof(SceneDictionary))]
    public class SceneDictionaryDrawer : DictionaryDrawer<ToolType, SceneAsset> { }
}