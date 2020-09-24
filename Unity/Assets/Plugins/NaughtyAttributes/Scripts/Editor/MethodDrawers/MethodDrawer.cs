using System.Reflection;

namespace Plugins.NaughtyAttributes.Scripts.Editor.MethodDrawers
{
    public abstract class MethodDrawer
    {
        public abstract void DrawMethod(UnityEngine.Object target, MethodInfo methodInfo);
    }
}
