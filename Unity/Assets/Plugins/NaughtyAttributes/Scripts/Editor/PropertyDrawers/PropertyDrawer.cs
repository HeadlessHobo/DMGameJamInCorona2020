using UnityEditor;

namespace Plugins.NaughtyAttributes.Scripts.Editor.PropertyDrawers
{
    public abstract class PropertyDrawer
    {
        public abstract void DrawProperty(SerializedProperty property);

        public virtual void ClearCache()
        {

        }
    }
}
