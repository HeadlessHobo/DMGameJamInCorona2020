using Common.Menu.TransitionEffects;
using Editor.Utils;
using UnityEngine.UIElements;

namespace Editor.GameSettingsWindow.SettingEntries
{
    public class TransitionsSettingEntry : SettingEntryBase
    {
        public const string TRANSITION_EFFECTS_DATA_SCRIPTABLE_OBJECT_NAME = "TransitionEffectsData";
        
        public TransitionsSettingEntry(GameSettingsWindow gameSettingsWindow) : base(gameSettingsWindow)
        {
        }

        public override VisualElement CreateLeftPanelEntryUI()
        {
            return UICreator.CreateLabelWithCallback("Transitions", OnLabelClicked);
        }
        
        private void OnLabelClicked(VisualElement label)
        {
            OnElementClicked(label, CreateContentUi());
        }

        private VisualElement CreateContentUi()
        {
            return ScriptableObjectUiUtils.CreateUiForScriptableObject<TransitionEffectsData>(TRANSITION_EFFECTS_DATA_SCRIPTABLE_OBJECT_NAME);
        }
    }
}