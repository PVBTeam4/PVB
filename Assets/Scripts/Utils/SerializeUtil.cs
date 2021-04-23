using System;
using UnityEditor;

namespace Utils
{
    /// <summary>
    /// Util class for serialized 
    /// </summary>
    public static class SerializeUtil
    {
        /// <summary>
        /// Set private serialized fields of Unity object
        /// </summary>
        /// <param name="unityObject"></param>
        /// <param name="serializedFieldsSetter"></param>
        public static void SetSerializedFields(UnityEngine.Object unityObject, Action<SerializedObject> serializedFieldsSetter)
        {
            SerializedObject serializedObject = new SerializedObject(unityObject);
            serializedFieldsSetter.Invoke(serializedObject);
            serializedObject.ApplyModifiedProperties();
        }

        public static SerializedProperty GetSerializedField(UnityEngine.Object unityObject, string fieldName)
        {
            SerializedObject serializedObject = new SerializedObject(unityObject);
            return serializedObject.FindProperty(fieldName);
        }
    }
}
