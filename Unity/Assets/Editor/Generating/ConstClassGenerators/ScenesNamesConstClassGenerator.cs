using System.Collections.Generic;
using System.IO;
using Plugins.Enums;
using UnityEditor;

namespace Editor.Generating.ConstClassGenerators
{
    public class ScenesNamesConstClassGenerator : SingleConstClassGenerator
    {
        protected override string GeneratedClassName => "Scenes";

        protected override IEnumerable<ConstFieldData> GenerateFieldDataEntries()
        {
            foreach (var scene in EditorBuildSettings.scenes)
            {
                string sceneName = Path.GetFileNameWithoutExtension(scene.path);
                yield return new ConstFieldData()
                {
                    Name = sceneName.ToUpper(),
                    DataType = BuiltInDataType.String,
                    Value = sceneName
                };
            }
        }
    }
}