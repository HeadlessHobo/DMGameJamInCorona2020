using System;
using System.Linq;
using System.Reflection;
using Common.Util;
using Editor.GameSettingsWindow;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Utils
{
    public static class ScriptableObjectUiUtils
    {
        public static VisualElement CreateUiForScriptableObject<T>(string name) where T : ScriptableObject
        {
            ScriptableObject scriptableObject = EditorScriptableObjectLoadSaveManager.Load<T>(name);
            Type scriptableObjType = scriptableObject.GetType();
            
            SerializedObject serializedScriptableObject = new SerializedObject(scriptableObject);

            VisualElement container = new VisualElement();
            var properties = scriptableObjType.GetFields(BindingFlags.NonPublic | 
                                                         BindingFlags.Instance)
                .Where(prop => prop.IsDefined(typeof(SerializeField), false));
            foreach (var property in properties)
            {
                PropertyField propertyField = new PropertyField(serializedScriptableObject.FindProperty(property.Name));   
                container.Add(propertyField);
                propertyField.Bind(serializedScriptableObject);
            }

            return container;
        }
    }
}