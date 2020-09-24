using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Common.Extensions
{
    public static class ReflectionExtensions
    {
        public static IEnumerable<Type> GetAllTypesThatInheritType<T>() where T : class
        {
            return Assembly.GetAssembly(typeof(T)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T)));
        }
    }
}