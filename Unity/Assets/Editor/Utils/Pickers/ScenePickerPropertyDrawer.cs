using System.Collections.Generic;
using System.IO;
using Common.Util.Pickers;
using UnityEditor;

namespace Editor.Utils.Pickers
{
    [CustomPropertyDrawer(typeof(ScenePicker), true)]
    public class ScenePickerPropertyDrawer : PickerPropertyDrawer
    {
        protected override List<string> GetPickerValues()
        {
            List<string> allScenes = new List<string>();
#if  UNITY_EDITOR
            foreach (var scene in EditorBuildSettings.scenes)
            {
                allScenes.Add(Path.GetFileNameWithoutExtension(scene.path));
            }
#endif
            return allScenes;
        }
    }
}