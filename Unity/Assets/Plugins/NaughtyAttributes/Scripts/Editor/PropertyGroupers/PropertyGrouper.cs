namespace Plugins.NaughtyAttributes.Scripts.Editor.PropertyGroupers
{
    public abstract class PropertyGrouper
    {
        public abstract void BeginGroup(string label);

        public abstract void EndGroup();
    }
}
