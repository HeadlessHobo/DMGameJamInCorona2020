using System.Reflection;

namespace Plugins.NaughtyAttributes.Scripts.Editor.NativePropertyDrawers
{
    public abstract class NativePropertyDrawer
    {
        public abstract void DrawNativeProperty(UnityEngine.Object target, PropertyInfo property);
    }
}
