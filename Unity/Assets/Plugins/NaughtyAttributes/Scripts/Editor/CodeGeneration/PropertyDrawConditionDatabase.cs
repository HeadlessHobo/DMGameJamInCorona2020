// This class is auto generated

using System;
using System.Collections.Generic;
using Plugins.NaughtyAttributes.Scripts.Core.DrawConditionAttributes;
using Plugins.NaughtyAttributes.Scripts.Editor.PropertyDrawConditions;

namespace Plugins.NaughtyAttributes.Scripts.Editor.CodeGeneration
{
    public static class PropertyDrawConditionDatabase
    {
        private static Dictionary<Type, PropertyDrawCondition> drawConditionsByAttributeType;

        static PropertyDrawConditionDatabase()
        {
            drawConditionsByAttributeType = new Dictionary<Type, PropertyDrawCondition>();
            drawConditionsByAttributeType[typeof(HideIfAttribute)] = new HideIfPropertyDrawCondition();
drawConditionsByAttributeType[typeof(ShowIfAttribute)] = new ShowIfPropertyDrawCondition();

        }

        public static PropertyDrawCondition GetDrawConditionForAttribute(Type attributeType)
        {
            PropertyDrawCondition drawCondition;
            if (drawConditionsByAttributeType.TryGetValue(attributeType, out drawCondition))
            {
                return drawCondition;
            }
            else
            {
                return null;
            }
        }
    }
}

