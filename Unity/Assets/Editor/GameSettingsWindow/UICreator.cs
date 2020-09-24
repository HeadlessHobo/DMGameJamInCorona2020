using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.GameSettingsWindow
{
    public delegate void ElementCallback(VisualElement element);
    
    public class UICreator
    {
        public static VisualElement CreateFoldoutWithMultipleLabels(string foldoutText, ElementCallback onClicked, params string[] labelNames)
        {
            Foldout foldout = CreateFoldoutWithCallback(foldoutText, onClicked);

            foreach (var labelName in labelNames)
            {
                foldout.Add(CreateLabelWithCallback(labelName, onClicked));
            }
            
            return foldout;
        }

        public static VisualElement CreateLabelWithCallback(string labelName, ElementCallback onClicked)
        {
            Label label = new Label(labelName);
            label.AddToClassList(UssClasses.MIDDLE_LEFT_TEXT_ALIGN);
            label.RegisterCallback<MouseDownEvent>(evt => onClicked(label));
            return label;
        }

        private static Foldout CreateFoldoutWithCallback(string label, ElementCallback onClicked)
        {
            Foldout foldout = new Foldout()
            {
                text = label
            };
            
            foldout.RegisterCallback<ChangeEvent<bool>>(evt => onClicked(foldout));
            return foldout;
        }

    }
}