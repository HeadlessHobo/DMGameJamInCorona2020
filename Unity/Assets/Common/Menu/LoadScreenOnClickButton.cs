using System.IO;
using Common.Util.Pickers;
using Plugins.NaughtyAttributes.Scripts.Core.DrawerAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Menu
{
    [ExecuteInEditMode]
    public class LoadScreenOnClickButton : ButtonBase
    {
        [SerializeField]
        private ScreenPicker _screenPicker;

        protected override void OnClick()
        {
            ScreenManager.Instance.LoadScreen(_screenPicker.PickedValue);
        }
    }
}
