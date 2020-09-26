using System.Collections.Generic;
using Common.SpawnHanding;
using Editor.Utils;
using UnityEngine.UIElements;

namespace Editor.GameSettingsWindow.SettingEntries
{
    public class MappingsSettingsEntry : SettingEntryBase
    {
        private const string SPAWN_TYPE_TO_PREFAB_MAPPING_LABEL_NAME = "Spawn type to prefab";
        
        private static VisualElement _rightPanel;
        private static Dictionary<string, VisualElement> _labelToContentUi;
        
        public MappingsSettingsEntry(GameSettingsWindow gameSettingsWindow) : base(gameSettingsWindow)
        {
            _labelToContentUi = new Dictionary<string, VisualElement>();
            _labelToContentUi.Add(SPAWN_TYPE_TO_PREFAB_MAPPING_LABEL_NAME, 
                ScriptableObjectUiUtils.CreateUiForScriptableObject<SpawnTypeToPrefabMapping>("SpawnTypeToPrefabMappings"));
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
            return UICreator.CreateFoldoutWithMultipleLabels("Mappings", OnLabelClicked, SPAWN_TYPE_TO_PREFAB_MAPPING_LABEL_NAME);
        }
    }

}