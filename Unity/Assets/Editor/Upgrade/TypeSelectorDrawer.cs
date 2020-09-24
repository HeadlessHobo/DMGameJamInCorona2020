using System;
using Common.Util;
using UnityEditor;
using UnityEngine;

namespace Editor.Upgrade
{
    public  abstract class TypeSelectorDrawer : PropertyDrawer
    {
        private SerializedProperty _upgradeActionIndexProperty;
        private SerializedProperty _upgradeActionNamesProperty;
        
        protected abstract string IndexPropertyName { get; }
        protected abstract string NamesPropertyName { get; }
        protected abstract Type SelectorType { get; }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SetProperties(property);
            _upgradeActionNamesProperty.SetStringArray(TypeUtil.GetNamesOfAllThatInheritFromType(SelectorType));
            _upgradeActionIndexProperty.intValue = EditorGUI.Popup(position, _upgradeActionIndexProperty.intValue, _upgradeActionNamesProperty.GetStringArray()); 
        }

        private void SetProperties(SerializedProperty property)
        {
            _upgradeActionIndexProperty = property.FindPropertyRelative(IndexPropertyName);
            _upgradeActionNamesProperty = property.FindPropertyRelative(NamesPropertyName);
        }
    }
}