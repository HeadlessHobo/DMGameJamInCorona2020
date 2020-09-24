using System.Collections.Generic;
using Editor.GameSettingsWindow.SettingEntries;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.GameSettingsWindow
{
    public class GameSettingsWindow : EditorWindow
    {
        private VisualElement _rightPanel;
        private List<SettingEntryBase> _settingEntries;
        
        [MenuItem("Tools/GameSettingsWindow")]
        public static void ShowExample()
        {
            GameSettingsWindow wnd = GetWindow<GameSettingsWindow>();
            wnd.titleContent = new GUIContent("GameSettingsWindow");
        }

        public void OnEnable()
        {
            CreateSettingEntries();
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/GameSettingsWindow/GameSettingsWindow.uxml");
            VisualElement labelFromUXML = visualTree.CloneTree();
            root.Add(labelFromUXML);

            _rightPanel = root.Q<VisualElement>("RightPanel");

            VisualElement leftPanel = root.Q<VisualElement>("LeftPanel");
            foreach (var settingEntry in _settingEntries)
            {
                leftPanel.Add(settingEntry.CreateLeftPanelEntryUI());
            }
            SelectedManager.RootElement = leftPanel;

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var gameSettingsStyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/GameSettingsWindow/GameSettingsWindow.uss");
            root.styleSheets.Add(gameSettingsStyleSheet);
            var statsTemplateStyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/GameSettingsWindow/Stats/StatsTemplate.uss");
            root.styleSheets.Add(statsTemplateStyleSheet);
        }

        private void CreateSettingEntries()
        {
            _settingEntries = new List<SettingEntryBase>()
            {
                new StatManagersSettingEntry(this),
                new ScreenLoadersSettingEntry(this),
                new TransitionsSettingEntry(this),
                new GameSettingsEntry(this)
            };
        }

        public void UpdateRightPanel(VisualElement contentRoot)
        {
            _rightPanel.Clear();
            _rightPanel.contentContainer.Add(contentRoot);
        }

        private void OnGUI()
        {
            // Set the container height to the window
            rootVisualElement.Q<VisualElement>("Container").style.height = new 
                StyleLength(position.height);
        }
    }
}