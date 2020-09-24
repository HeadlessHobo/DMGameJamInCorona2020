using System.Collections.Generic;
using System.IO;
using Common.Menu.TransitionEffects;
using Common.Util;
using Common.Util.Pickers;
using Editor.GameSettingsWindow;
using Editor.GameSettingsWindow.SettingEntries;
using UnityEditor;
using UnityEngine;

namespace Editor.Utils.Pickers
{
    [CustomPropertyDrawer(typeof(TransitionEffectPicker))]
    public class TransitionEffectPickerPropertyDrawer : PickerPropertyDrawer
    {
        protected override List<string> GetPickerValues()
        {
            TransitionEffectsData transitionEffectsData =
                ScriptableObjectUtils.Load<TransitionEffectsData>(TransitionsSettingEntry
                    .TRANSITION_EFFECTS_DATA_SCRIPTABLE_OBJECT_NAME);
            
            return transitionEffectsData.GetAllTransitionEffectNames();
        }
    }
}