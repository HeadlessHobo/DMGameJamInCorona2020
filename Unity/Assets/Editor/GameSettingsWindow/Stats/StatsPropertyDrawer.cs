using Common.UnitSystem;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.GameSettingsWindow.Stats
{
    [CustomPropertyDrawer(typeof(Stat))]
    public class StatsPropertyDrawer : PropertyDrawer
    {
        private Foldout _statFoldout;
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            _statFoldout = new Foldout()
            {
                text = property.displayName,
            };
            _statFoldout.Add(new PropertyField(property.FindPropertyRelative("_startStat")));
            SerializedProperty showMinMaxProperty = property.FindPropertyRelative("_usesMinMax");
            
            PropertyField minMaxPropertyField = new PropertyField(showMinMaxProperty);
            _statFoldout.Add(minMaxPropertyField);
            if (showMinMaxProperty.boolValue)
            {
                _statFoldout.Add(new PropertyField(property.FindPropertyRelative("_minStatValue")));
                _statFoldout.Add(new PropertyField(property.FindPropertyRelative("_maxStatValue")));
            }

            return _statFoldout;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
              
        }
    }
}