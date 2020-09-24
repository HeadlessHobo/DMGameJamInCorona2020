using UnityEngine.UIElements;

namespace Editor.GameSettingsWindow
{
    public class SelectedManager
    {
        public static VisualElement RootElement { get; set; }
        public static void UpdateSelectedStatus(VisualElement newSelectedElement)
        {
            DeselectAllElements(RootElement);
            newSelectedElement.AddToClassList(UssClasses.SELECTED);
        }

        private static void DeselectAllElements(VisualElement rootElement)
        {
            foreach (var childElement in rootElement.Children())
            {
                if (childElement.childCount > 0)
                {
                    DeselectAllElements(childElement);
                }
                
                childElement.RemoveFromClassList(UssClasses.SELECTED);
            }
        }
    }
}