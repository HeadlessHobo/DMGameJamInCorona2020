using UnityEditor;

namespace Plugins.NaughtyAttributes.Scripts.Editor.PropertyDrawConditions
{
    public abstract class PropertyDrawCondition
    {
        public abstract bool CanDrawProperty(SerializedProperty property);
    }
}
