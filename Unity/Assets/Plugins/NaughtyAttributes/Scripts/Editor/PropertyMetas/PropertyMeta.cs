using Plugins.NaughtyAttributes.Scripts.Core.MetaAttributes;
using UnityEditor;

namespace Plugins.NaughtyAttributes.Scripts.Editor.PropertyMetas
{
    public abstract class PropertyMeta
    {
        public abstract void ApplyPropertyMeta(SerializedProperty property, MetaAttribute metaAttribute);
    }
}
