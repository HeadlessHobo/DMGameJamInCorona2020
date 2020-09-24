using Plugins.NaughtyAttributes.Scripts.Core.DrawerAttributes;
using Plugins.NaughtyAttributes.Scripts.Editor.Attributes;

namespace Plugins.NaughtyAttributes.Scripts.Editor.PropertyDrawers
{
    [PropertyDrawer(typeof(DisableIfAttribute))]
    public class DisableIfPropertyDrawer : EnableIfPropertyDrawer
    {
    }
}
