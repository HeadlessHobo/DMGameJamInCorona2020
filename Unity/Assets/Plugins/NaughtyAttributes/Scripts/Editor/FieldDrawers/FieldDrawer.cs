using System.Reflection;

namespace Plugins.NaughtyAttributes.Scripts.Editor.FieldDrawers
{
    public abstract class FieldDrawer
    {
        public abstract void DrawField(UnityEngine.Object target, FieldInfo field);
    }
}
