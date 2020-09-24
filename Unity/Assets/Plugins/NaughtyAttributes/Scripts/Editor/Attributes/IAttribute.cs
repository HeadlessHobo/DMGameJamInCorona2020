using System;

namespace Plugins.NaughtyAttributes.Scripts.Editor.Attributes
{
    public interface IAttribute
    {
        Type TargetAttributeType { get; }
    }
}
