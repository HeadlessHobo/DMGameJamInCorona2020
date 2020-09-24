using System.IO;
using Common.Menu.TransitionEffects;
using UnityEngine;

namespace Common.Util
{
    public static class ScriptableObjectUtils
    {
        public const string RESOURCES_SCRIPTABLE_GENERATED_OBJECTS_PATH = @"Data/GeneratedScriptableObjects";
        
        public static T Load<T>(string scriptableObjectName) where T : ScriptableObject
        {
            string scriptableObjectPath = Path.Combine(RESOURCES_SCRIPTABLE_GENERATED_OBJECTS_PATH, scriptableObjectName);
            return Resources.Load<T>(scriptableObjectPath);
        }
    }
}