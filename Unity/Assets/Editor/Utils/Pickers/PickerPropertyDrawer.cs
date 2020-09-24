using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Utils.Pickers
{
    public abstract class PickerPropertyDrawer : PropertyDrawer
    {
        private const string PICKED_VALUE_PROPERTY_NAME = "_pickedValue";
        
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty pickedValueProperty = property.FindPropertyRelative(PICKED_VALUE_PROPERTY_NAME);
            List<string> pickerValues = GetPickerValues();
            
            int index = Mathf.Max (0, pickerValues.IndexOf(pickedValueProperty.stringValue));
            index = EditorGUI.Popup (position, property.displayName, index, pickerValues.ToArray());

            pickedValueProperty.stringValue = FormatSelectedValue(pickerValues [index]);
        }
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement container = new VisualElement();
            container.style.flexDirection = FlexDirection.Row;

            Label nameLabel = new Label(property.displayName);
            nameLabel.style.flexGrow = 1;
            container.Add(nameLabel);

            VisualElement popupField = CreatePopupField(property);
            popupField.style.flexGrow = 5;
            container.Add(popupField);

            return container;
        }

        public override bool CanCacheInspectorGUI(SerializedProperty property)
        {
            return false;
        }

        private VisualElement CreatePopupField(SerializedProperty property)
        {
            List<string> pickerValues = GetPickerValues();
            if (pickerValues.Count <= 0)
            {
                return new Label("No items");
            }

            string defaultValue =
                pickerValues.Contains(property.FindPropertyRelative(PICKED_VALUE_PROPERTY_NAME).stringValue)
                    ? property.FindPropertyRelative(PICKED_VALUE_PROPERTY_NAME).stringValue
                    : pickerValues.First();
            
            PopupField<string> popupField = new PopupField<string>(pickerValues, defaultValue);
            popupField.RegisterValueChangedCallback(evt => OnNewValuePicked(evt, property));
            return popupField;
        }

        private void OnNewValuePicked(ChangeEvent<string> evt, SerializedProperty serializedProperty)
        {
            serializedProperty.FindPropertyRelative(PICKED_VALUE_PROPERTY_NAME).stringValue = evt.newValue;
            serializedProperty.serializedObject.ApplyModifiedProperties();
        }

        protected abstract List<string> GetPickerValues();

        protected virtual string FormatSelectedValue(string selectedValue)
        {
            return selectedValue;
        }
    }
}