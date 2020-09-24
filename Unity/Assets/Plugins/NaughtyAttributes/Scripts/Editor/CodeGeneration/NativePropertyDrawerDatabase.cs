// This class is auto generated

using System;
using System.Collections.Generic;
using Plugins.NaughtyAttributes.Scripts.Core.DrawerAttributes;
using Plugins.NaughtyAttributes.Scripts.Editor.NativePropertyDrawers;

namespace Plugins.NaughtyAttributes.Scripts.Editor.CodeGeneration
{
    public static class NativePropertyDrawerDatabase
    {
        private static Dictionary<Type, NativePropertyDrawer> drawersByAttributeType;

        static NativePropertyDrawerDatabase()
        {
            drawersByAttributeType = new Dictionary<Type, NativePropertyDrawer>();
            drawersByAttributeType[typeof(ShowNativePropertyAttribute)] = new ShowNativePropertyNativePropertyDrawer();

        }

        public static NativePropertyDrawer GetDrawerForAttribute(Type attributeType)
        {
            NativePropertyDrawer drawer;
            if (drawersByAttributeType.TryGetValue(attributeType, out drawer))
            {
                return drawer;
            }
            else
            {
                return null;
            }
        }
    }
}

