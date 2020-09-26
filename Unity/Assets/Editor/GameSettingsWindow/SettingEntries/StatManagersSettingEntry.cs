using System.Collections.Generic;
using Common.UnitSystem.ExamplePlayer.Stats;
using Editor.Utils;
using Enemies;
using Player.Tnts;
using UnityEngine.UIElements;

namespace Editor.GameSettingsWindow.SettingEntries
{
    public class StatManagersSettingEntry : SettingEntryBase
    {
        private const string PLAYER_STATS_MANAGER_LABEL_NAME = "Player";
        private const string EXPLOSION_STATS_MANAGER_LABEL_NAME = "Explosion";
        private const string DANE_STATS_MANAGER_LABEL_NAME = "Dane";
        private const string TNT_STATS_MANAGER_LABEL_NAME = "TNT";
        
        private static VisualElement _rightPanel;
        private static Dictionary<string, VisualElement> _labelToContentUi;
        
        public StatManagersSettingEntry(GameSettingsWindow gameSettingsWindow) : base(gameSettingsWindow)
        {
            _labelToContentUi = new Dictionary<string, VisualElement>();
            _labelToContentUi.Add(PLAYER_STATS_MANAGER_LABEL_NAME, ScriptableObjectUiUtils.CreateUiForScriptableObject<PlayerStatsManager>("Player"));
            _labelToContentUi.Add(EXPLOSION_STATS_MANAGER_LABEL_NAME, ScriptableObjectUiUtils.CreateUiForScriptableObject<ExplosionStatsManager>("Explosion"));
            _labelToContentUi.Add(DANE_STATS_MANAGER_LABEL_NAME, ScriptableObjectUiUtils.CreateUiForScriptableObject<DaneStatsManager>("Dane"));
            _labelToContentUi.Add(TNT_STATS_MANAGER_LABEL_NAME, ScriptableObjectUiUtils.CreateUiForScriptableObject<TNTStatsManager>("TNT"));
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
            return UICreator.CreateFoldoutWithMultipleLabels("Stats", OnLabelClicked, PLAYER_STATS_MANAGER_LABEL_NAME,
                EXPLOSION_STATS_MANAGER_LABEL_NAME, DANE_STATS_MANAGER_LABEL_NAME, TNT_STATS_MANAGER_LABEL_NAME);
        }
    }
}