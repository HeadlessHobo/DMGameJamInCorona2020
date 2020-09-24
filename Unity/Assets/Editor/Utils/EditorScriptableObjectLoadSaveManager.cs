using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace Common.Util
{
    public class EditorScriptableObjectLoadSaveManager
    {
        private const string RELATIVE_SCRIPTABLE_OBJECTS_SAVE_PATH = @"Assets/Resources/Data/GeneratedScriptableObjects";

        public static T Load<T>(string name) where T : ScriptableObject
        {
            Type scriptableObjectType = typeof(T);
            CreateScriptableObjectIfNotExists(name, scriptableObjectType);

            return (T) LoadScriptableObject(name, scriptableObjectType);
        }
        
        public static void Save<T>(string name)
        {
            Type scriptableObjectType = typeof(T);
            CreateScriptableObjectIfNotExists(name, scriptableObjectType);
        }

        private static void CreateScriptableObjectIfNotExists(string name, Type scriptableObjectType)
        {
            string scriptableObjPath = GetAbsoluteScriptableObjectPath(name);
            if (!File.Exists(scriptableObjPath))
            {
                ScriptableObject scriptableObjectInstance = ScriptableObject.CreateInstance(scriptableObjectType);
                AssetDatabase.CreateAsset (scriptableObjectInstance, GetRelativePath(name));
                AssetDatabase.SaveAssets ();
            }
        }

        private static string GetAbsoluteScriptableObjectPath(string name)
        {
            return Path.Combine(Application.dataPath,"../", GetRelativePath(name));
        }

        private static string GetRelativePath(string name)
        {
            return Path.Combine(RELATIVE_SCRIPTABLE_OBJECTS_SAVE_PATH, name + ".asset");
        }

        private static Object LoadScriptableObject(string name, Type scriptableObjectType)
        {
            return AssetDatabase.LoadAssetAtPath(GetRelativePath(name), scriptableObjectType);
        }

    }
}