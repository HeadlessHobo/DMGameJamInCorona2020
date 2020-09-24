using System;
using System.Linq;

namespace Common.Util
{
    public class TypeUtil
    {
        public static string[] GetNamesOfAllThatInheritFromType(Type type)
        {
            var typesThatImplementUpgradeAction = GetTypesThatImplementType(type);

            return typesThatImplementUpgradeAction.Select(item => item.ToString()).ToArray();
        }
        
        public static Type[] GetTypesThatImplementType(Type typeToImplement)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(type => typeToImplement.IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface).ToArray(); 
        }
    }
}