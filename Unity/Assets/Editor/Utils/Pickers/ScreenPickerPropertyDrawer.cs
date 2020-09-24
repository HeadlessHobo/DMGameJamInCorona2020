using System.Collections.Generic;
using Common;
using Common.Menu;
using Common.Util.Pickers;
using UnityEditor;
using UnityEngine;

namespace Editor.Utils.Pickers
{
    [CustomPropertyDrawer(typeof(ScreenPicker))]
    public class ScreenPickerPropertyDrawer : PickerPropertyDrawer
    {
        protected override List<string> GetPickerValues()
        {
            GameObject managersPrefab =  Resources.Load<GameObject>(GameObjectsManager.MANAGERS_PREFAB_PATH);
            ScreenManager screenManager = managersPrefab.GetComponentInChildren<ScreenManager>();
            
            return screenManager.GetAllScreenNames();
        }
    }
}