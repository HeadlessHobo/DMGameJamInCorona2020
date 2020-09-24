using System.Collections.Generic;

namespace Editor.Generating
{
    public abstract class SingleConstClassGenerator : IConstClassGenerator
    {
        protected abstract string GeneratedClassName { get; }

        public void Generate()
        {
            ConstClassGeneratorUtil.GenerateClass(GeneratedClassName, GenerateFieldDataEntries());
        }

        protected abstract IEnumerable<ConstFieldData> GenerateFieldDataEntries();
    }
}