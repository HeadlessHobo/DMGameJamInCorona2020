using System.Collections.Generic;
using Common.UnitSystem.ExamplePlayer.Stats;
using Editor.Utils;
using UnityEngine.UIElements;

namespace Editor.GameSettingsWindow.SettingEntries
{
    public class StatManagersSettingEntry : SettingEntryBase
    {
        private const string PLAYER_STATS_MANAGER_LABEL_NAME = "Player";
        
        private static VisualElement _rightPanel;
        private static Dictionary<string, VisualElement> _labelToContentUi;
        
        public StatManagersSettingEntry(GameSettingsWindow gameSettingsWindow) : base(gameSettingsWindow)
        {
            _labelToContentUi = new Dictionary<string, VisualElement>();
            _labelToContentUi.Add(PLAYER_STATS_MANAGER_LABEL_NAME, ScriptableObjectUiUtils.CreateUiForScriptableObject<ExamplePlayerStatsManager>("Player"));
        }

        private void OnLabelClicked(VisualElement element)
        {
            if (element is Label label)
            {
                OnElementClicked(element, _labelToContentUi[label.text]);
            }
        }

        public override VisualElement CreateLeftPanelEntryUI()
        {
            return UICreator.CreateFoldoutWithMultipleLabels("Stats", OnLabelClicked, PLAYER_STATS_MANAGER_LABEL_NAME);
        }
    }
}