using UnityEngine.UIElements;

namespace Editor.GameSettingsWindow.SettingEntries
{
    public abstract class SettingEntryBase
    {
        private GameSettingsWindow _gameSettingsWindow;
        
        protected SettingEntryBase(GameSettingsWindow gameSettingsWindow)
        {
            _gameSettingsWindow = gameSettingsWindow;
        }

        public abstract VisualElement CreateLeftPanelEntryUI();

        protected void OnElementClicked(VisualElement elementClicked, VisualElement contentRoot)
        {
            _gameSettingsWindow.UpdateRightPanel(contentRoot);
            SelectedManager.UpdateSelectedStatus(elementClicked);
        }
    }
}