using System.Reflection;
using Plugins.NaughtyAttributes.Scripts.Core.DrawerAttributes;
using Plugins.NaughtyAttributes.Scripts.Editor.Attributes;
using Plugins.NaughtyAttributes.Scripts.Editor.Utility;
using UnityEditor;
using UnityEngine;

namespace Plugins.NaughtyAttributes.Scripts.Editor.MethodDrawers
{
    [MethodDrawer(typeof(ButtonAttribute))]
    public class ButtonMethodDrawer : MethodDrawer
    {
        public override void DrawMethod(UnityEngine.Object target, MethodInfo methodInfo)
        {
            if (methodInfo.GetParameters().Length == 0)
            {
                ButtonAttribute buttonAttribute = (ButtonAttribute)methodInfo.GetCustomAttributes(typeof(ButtonAttribute), true)[0];
                string buttonText = string.IsNullOrEmpty(buttonAttribute.Text) ? methodInfo.Name : buttonAttribute.Text;

                if (GUILayout.Button(buttonText))
                {
                    methodInfo.Invoke(target, null);
                }
            }
            else
            {
                string warning = typeof(ButtonAttribute).Name + " works only on methods with no parameters";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, context: target);
            }
        }
    }
}
