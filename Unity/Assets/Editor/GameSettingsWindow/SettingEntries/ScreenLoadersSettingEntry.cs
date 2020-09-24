using Common.Menu.ScreenLoaders;
using Editor.Utils;
using UnityEngine.UIElements;

namespace Editor.GameSettingsWindow.SettingEntries
{
    public class ScreenLoadersSettingEntry : SettingEntryBase
    {
        public ScreenLoadersSettingEntry(GameSettingsWindow gameSettingsWindow) : base(gameSettingsWindow)
        {
        }

        public override VisualElement CreateLeftPanelEntryUI()
        {
            return UICreator.CreateLabelWithCallback("ScreenLoaders", OnLabelClicked);
        }

        private void OnLabelClicked(VisualElement label)
        {
            OnElementClicked(label, CreateContentUi());
        }

        private VisualElement CreateContentUi()
        {
            return ScriptableObjectUiUtils.CreateUiForScriptableObject<ScreenLoadersData>("ScreenLoaders");
        }
    }
}