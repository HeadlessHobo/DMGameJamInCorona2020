using UnityEditor;
using UnityEngine.UIElements;

namespace Editor.Utils.Button
{
    public abstract class ButtonPropertyDrawer : PropertyDrawer
    {
        protected abstract string ButtonName { get; }
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            UnityEngine.UIElements.Button button = new UnityEngine.UIElements.Button(() => OnButtonClicked(property));
            button.text = ButtonName;
            return button;
        }

        protected abstract void OnButtonClicked(SerializedProperty property);
    }
}