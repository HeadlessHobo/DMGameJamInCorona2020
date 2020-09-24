using Plugins.NaughtyAttributes.Scripts.Core.ValidatorAttributes;
using Plugins.NaughtyAttributes.Scripts.Editor.Attributes;
using Plugins.NaughtyAttributes.Scripts.Editor.Utility;
using UnityEditor;

namespace Plugins.NaughtyAttributes.Scripts.Editor.PropertyValidators
{
    [PropertyValidator(typeof(MaxValueAttribute))]
    public class MaxValuePropertyValidator : PropertyValidator
    {
        public override void ValidateProperty(SerializedProperty property)
        {
            MaxValueAttribute maxValueAttribute = PropertyUtility.GetAttribute<MaxValueAttribute>(property);

            if (property.propertyType == SerializedPropertyType.Integer)
            {
                if (property.intValue > maxValueAttribute.MaxValue)
                {
                    property.intValue = (int)maxValueAttribute.MaxValue;
                }
            }
            else if (property.propertyType == SerializedPropertyType.Float)
            {
                if (property.floatValue > maxValueAttribute.MaxValue)
                {
                    property.floatValue = maxValueAttribute.MaxValue;
                }
            }
            else
            {
                string warning = maxValueAttribute.GetType().Name + " can be used only on int or float fields";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, context: PropertyUtility.GetTargetObject(property));
            }
        }
    }
}
