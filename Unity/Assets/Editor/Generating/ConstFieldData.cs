using Plugins.Enums;

namespace Editor.Generating
{
    public class ConstFieldData
    {
        public string Name { get; set; }
        public BuiltInDataType DataType { get; set; }
        public object Value { get; set; }
    }
}