using Plugins.NaughtyAttributes.Scripts.Core.DrawConditionAttributes;
using Plugins.NaughtyAttributes.Scripts.Editor.Attributes;

namespace Plugins.NaughtyAttributes.Scripts.Editor.PropertyDrawConditions
{
    [PropertyDrawCondition(typeof(HideIfAttribute))]
    public class HideIfPropertyDrawCondition : ShowIfPropertyDrawCondition
    {
    }
}
