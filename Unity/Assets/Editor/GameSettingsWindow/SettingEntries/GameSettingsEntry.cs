using Common;
using Common.Menu.ScreenLoaders;
using Editor.Utils;
using UnityEngine.UIElements;

namespace Editor.GameSettingsWindow.SettingEntries
{
    public class GameSettingsEntry : SettingEntryBase
    {
        public GameSettingsEntry(GameSettingsWindow gameSettingsWindow) : base(gameSettingsWindow)
        {
        }

        public override VisualElement CreateLeftPanelEntryUI()
        {
            return UICreator.CreateLabelWithCallback("Game settings", OnLabelClicked);
        }
        
        private void OnLabelClicked(VisualElement label)
        {
            OnElementClicked(label, CreateContentUi());
        }

        private VisualElement CreateContentUi()
        {
            return ScriptableObjectUiUtils.CreateUiForScriptableObject<GameSettings>(GameManager.GAME_SETTINGS_SCRIPTABLE_OBJECT_NAME);
        }
    }
}