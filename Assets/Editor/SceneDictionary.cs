using Global;
using System;
using UnityEditor;

/// <summary>
/// This Dictionary Class (extended from SerializableDictionary) is used to link Scenes to the ToolType enum
/// </summary>
[Serializable]
public class SceneDictionary : SerializableDictionary<ToolType, SceneAsset> { }