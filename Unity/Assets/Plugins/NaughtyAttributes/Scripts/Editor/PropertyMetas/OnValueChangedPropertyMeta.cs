using System.Reflection;
using Plugins.NaughtyAttributes.Scripts.Core.MetaAttributes;
using Plugins.NaughtyAttributes.Scripts.Editor.Attributes;
using Plugins.NaughtyAttributes.Scripts.Editor.Utility;
using UnityEditor;
using UnityEngine;

namespace Plugins.NaughtyAttributes.Scripts.Editor.PropertyMetas
{
    [PropertyMeta(typeof(OnValueChangedAttribute))]
    public class OnValueChangedPropertyMeta : PropertyMeta
    {
        public override void ApplyPropertyMeta(SerializedProperty property, MetaAttribute metaAttribute)
        {
            OnValueChangedAttribute onValueChangedAttribute = (OnValueChangedAttribute)metaAttribute;
            UnityEngine.Object target = PropertyUtility.GetTargetObject(property);

            MethodInfo callbackMethod = ReflectionUtility.GetMethod(target, onValueChangedAttribute.CallbackName);
            if (callbackMethod != null &&
                callbackMethod.ReturnType == typeof(void) &&
                callbackMethod.GetParameters().Length == 0)
            {
                property.serializedObject.ApplyModifiedProperties(); // We must apply modifications so that the callback can be invoked with up-to-date data

                callbackMethod.Invoke(target, null);
            }
            else
            {
                string warning = onValueChangedAttribute.GetType().Name + " can invoke only action methods - with void return type and no parameters";
                Debug.LogWarning(warning, target);
            }
        }
    }
}
