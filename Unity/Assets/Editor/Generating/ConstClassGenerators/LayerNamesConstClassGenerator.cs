using System.Collections.Generic;
using Plugins.Enums;
using UnityEngine;

namespace Editor.Generating.ConstClassGenerators
{
    public class LayerNamesConstClassGenerator : SingleConstClassGenerator
    {
        protected override string GeneratedClassName => "Layers";

        protected override IEnumerable<ConstFieldData> GenerateFieldDataEntries()
        {
            for(int i=0;i<=31;i++) //user defined layers start with layer 8 and unity supports 31 layers
            {
                var layerName = LayerMask.LayerToName(i);
                if (layerName.Length > 0)
                {
                    yield return new ConstFieldData()
                    {
                        Name = layerName.ToUpper(),
                        DataType = BuiltInDataType.String,
                        Value = layerName
                    };
                }
            }
        }
    }
}