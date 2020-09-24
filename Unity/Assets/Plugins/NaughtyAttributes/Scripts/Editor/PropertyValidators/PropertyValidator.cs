using UnityEditor;

namespace Plugins.NaughtyAttributes.Scripts.Editor.PropertyValidators
{
    public abstract class PropertyValidator
    {
        public abstract void ValidateProperty(SerializedProperty property);
    }
}
