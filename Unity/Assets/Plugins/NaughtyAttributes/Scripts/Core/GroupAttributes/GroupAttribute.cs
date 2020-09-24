namespace Plugins.NaughtyAttributes.Scripts.Core.GroupAttributes
{
    public abstract class GroupAttribute : NaughtyAttribute
    {
        public string Name { get; private set; }

        public GroupAttribute(string name)
        {
            this.Name = name;
        }
    }
}
