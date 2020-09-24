using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Common;
using Common.Menu;
using Common.Menu.ScreenLoaders;
using Common.Util.Buttons;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;

namespace Editor.Utils.Button
{
    [CustomPropertyDrawer(typeof(_3DScreenLoaderButton))]
    public class _3DScreenLoaderPropertyDrawerButton : ButtonPropertyDrawer
    {
        protected override string ButtonName => "Apply";
        
        protected override void OnButtonClicked(SerializedProperty property)
        {
            GameObject managersPrefab =  Resources.Load<GameObject>(GameObjectsManager.MANAGERS_PREFAB_PATH);
            ScreenManager screenManager = managersPrefab.GetComponentInChildren<ScreenManager>();
            
            _3DScreenLoaderData _3dScreenLoaderData = (_3DScreenLoaderData)GetParent(property);
            screenManager.LoadScreenWithoutTransitionsFromEditor(_3dScreenLoaderData.ScreenName);
        }
        
        public object GetParent(SerializedProperty prop)
        {
            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach(var element in elements.Take(elements.Length-1))
            {
                if(element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[","").Replace("]",""));
                    obj = GetValue(obj, elementName, index);
                }
                else
                {
                    obj = GetValue(obj, element);
                }
            }
            return obj;
        }
 
        public object GetValue(object source, string name)
        {
            if(source == null)
                return null;
            var type = source.GetType();
            var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if(f == null)
            {
                var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if(p == null)
                    return null;
                return p.GetValue(source, null);
            }
            return f.GetValue(source);
        }
 
        public object GetValue(object source, string name, int index)
        {
            var enumerable = GetValue(source, name) as IEnumerable;
            var enm = enumerable.GetEnumerator();
            while(index-- >= 0)
                enm.MoveNext();
            return enm.Current;
        }
    }
}