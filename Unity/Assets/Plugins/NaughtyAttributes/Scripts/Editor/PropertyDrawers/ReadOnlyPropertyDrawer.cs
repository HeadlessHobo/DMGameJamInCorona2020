using Plugins.NaughtyAttributes.Scripts.Core.DrawerAttributes;
using Plugins.NaughtyAttributes.Scripts.Editor.Attributes;
using Plugins.NaughtyAttributes.Scripts.Editor.Utility;
using UnityEditor;
using UnityEngine;

namespace Plugins.NaughtyAttributes.Scripts.Editor.PropertyDrawers
{
    [PropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyPropertyDrawer : PropertyDrawer
    {
        public override void DrawProperty(SerializedProperty property)
        {
            GUI.enabled = false;
            EditorDrawUtility.DrawPropertyField(property);
            GUI.enabled = true;
        }
    }
}
