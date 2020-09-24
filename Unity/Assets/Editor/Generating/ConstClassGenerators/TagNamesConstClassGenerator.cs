using System.Collections.Generic;
using Plugins.Enums;

namespace Editor.Generating.ConstClassGenerators
{
    public class TagNamesConstClassGenerator : SingleConstClassGenerator
    {
        protected override string GeneratedClassName => "Tags";

        protected override IEnumerable<ConstFieldData> GenerateFieldDataEntries()
        {
            foreach (var tag in UnityEditorInternal.InternalEditorUtility.tags)
            {
                yield return new ConstFieldData()
                {
                    Name = tag.ToUpper(),
                    DataType = BuiltInDataType.String,
                    Value = tag
                };
            }
        }
    }
}