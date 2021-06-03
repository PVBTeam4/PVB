using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Properties.Tags;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Editor
{
    public class ParticleElement
    {
        private readonly string _name;
        public string Name => _name;

        private readonly ParticleType _particleType;
        public ParticleType ParticleType => _particleType;
        
        public ParticleElement(string name, ParticleType particleType)
        {
            _name = name;
            _particleType = particleType;
        }
    }
    
    [CustomPropertyDrawer(typeof(ParticleTypeSelectorAttribute))]
    public class ParticleTypeSelectorPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                EditorGUI.BeginProperty(position, label, property);
                
                // var attrib = attribute as ParticleTypeSelectorAttribute;
                
                //generate the taglist + custom tags
                List<ParticleElement> particleTypeList = new List<ParticleElement>();
                particleTypeList.Add(new ParticleElement("<NoParticle>", null));
                particleTypeList.AddRange(ConvertToParticleElements(GetParticleUtilFieldValues()));

                ParticleType propertyParticleType = property.objectReferenceValue as ParticleType;

                int index = -1;
                if (propertyParticleType == null)
                {
                    //The tag is empty
                    index = 0; //first index is the special <NoParticle> entry
                }
                else
                {
                    //check if there is an entry that matches the entry and get the index
                    //we skip index 0 as that is a special custom case
                    for (int i = 1; i < particleTypeList.Count; i++)
                    {
                        if (particleTypeList[i].ParticleType.ParticleName == propertyParticleType.ParticleName)
                        {
                            index = i;
                            break;
                        }
                    }
                }

                //Draw the popup box with the current selected index
                index = EditorGUI.Popup(position, label.text, index, ConvertToParticleNameArray(particleTypeList.ToArray()));

                //Adjust the actual string value of the property based on the selection
                if (index == 0)
                {
                    property.objectReferenceValue = null;
                }
                else if (index >= 1)
                {
                    // SetTargetObjectOfProperty(property, particleTypeList[index].ParticleType);
                    property.objectReferenceValue = particleTypeList[index].ParticleType;
                    Debug.Log("Set Object: " + GetTargetObjectOfProperty(property));
                }
                else
                {
                    property.objectReferenceValue = null;
                    // SetTargetObjectOfProperty(property, null);
                }
                
                EditorGUI.EndProperty();
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }

        private static string[] ConvertToParticleNameArray(ParticleElement[] particleElements)
        {
            string[] particleNames = new string[particleElements.Length];
            for (int i = 0; i < particleElements.Length; i++)
            {
                particleNames[i] = particleElements[i].Name;
            }
            return particleNames;
        }

        private static ParticleElement[] ConvertToParticleElements(Dictionary<string, ParticleType> dictionary)
        {
            List<ParticleElement> particleElements = new List<ParticleElement>();
            foreach (KeyValuePair<string,ParticleType> keyValuePair in dictionary)
            {
                particleElements.Add(new ParticleElement(keyValuePair.Key, keyValuePair.Value));
            }
            return particleElements.ToArray();
        }

        private static Dictionary<string, ParticleType> GetParticleUtilFieldValues()
        {
            FieldInfo[] fieldInfoArray = typeof(ParticleUtil).GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance)
                .Where(info => info.FieldType == typeof(ParticleType)).ToArray();

            return fieldInfoArray
                .ToDictionary(f => f.Name,
                    info =>
                    {
                        object value = info.GetValue(null);
                        Debug.Log(info.Name + ":" + info.FieldType + " | " + value);
                        return (ParticleType) value;
                    });
    }
        
        /// <summary>
        /// Gets the object the property represents.
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        private static object GetTargetObjectOfProperty(SerializedProperty prop)
        {
            if (prop == null) return null;

            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements)
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue_Imp(obj, elementName, index);
                }
                else
                {
                    obj = GetValue_Imp(obj, element);
                }
            }
            return obj;
        }
        
        private static void SetTargetObjectOfProperty(SerializedProperty prop, object value)
        {
            if (prop == null) return;

            string path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (string element in elements)
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "")
                        .Replace("]", ""));
                    SetValue_Imp(obj, elementName, index);
                }
                else
                {
                    SetValue_Imp(obj, element, value);
                }
            }
        }
        
        private static object GetValue_Imp(object source, string name)
        {
            if (source == null)
                return null;
            var type = source.GetType();

            while (type != null)
            {
                var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (f != null)
                    return f.GetValue(source);

                var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null)
                    return p.GetValue(source, null);

                type = type.BaseType;
            }
            return null;
        }
        
        private static void SetValue_Imp(object source, string name, object value)
        {
            if (source == null)
                return;
            var type = source.GetType();

            while (type != null)
            {
                var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (f != null)
                {
                    f.SetValue(source, value);
                    return;
                }

                var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null)
                {
                    p.SetValue(source, value);
                    return;
                }
                type = type.BaseType;
            }
        }

        private static object GetValue_Imp(object source, string name, int index)
        {
            var enumerable = GetValue_Imp(source, name) as System.Collections.IEnumerable;
            if (enumerable == null) return null;
            var enm = enumerable.GetEnumerator();
            //while (index-- >= 0)
            //    enm.MoveNext();
            //return enm.Current;

            for (int i = 0; i <= index; i++)
            {
                if (!enm.MoveNext()) return null;
            }
            return enm.Current;
        }
    }
}
