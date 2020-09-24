using Plugins.NaughtyAttributes.Scripts.Core.ValidatorAttributes;
using Plugins.NaughtyAttributes.Scripts.Editor.Attributes;
using Plugins.NaughtyAttributes.Scripts.Editor.Utility;
using UnityEditor;

namespace Plugins.NaughtyAttributes.Scripts.Editor.PropertyValidators
{
    [PropertyValidator(typeof(RequiredAttribute))]
    public class RequiredPropertyValidator : PropertyValidator
    {
        public override void ValidateProperty(SerializedProperty property)
        {
            RequiredAttribute requiredAttribute = PropertyUtility.GetAttribute<RequiredAttribute>(property);

            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                if (property.objectReferenceValue == null)
                {
                    string errorMessage = property.name + " is required";
                    if (!string.IsNullOrEmpty(requiredAttribute.Message))
                    {
                        errorMessage = requiredAttribute.Message;
                    }

                    EditorDrawUtility.DrawHelpBox(errorMessage, MessageType.Error, context: PropertyUtility.GetTargetObject(property));
                }
            }
            else
            {
                string warning = requiredAttribute.GetType().Name + " works only on reference types";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, context: PropertyUtility.GetTargetObject(property));
            }
        }
    }
}
