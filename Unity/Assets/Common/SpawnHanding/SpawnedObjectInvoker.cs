using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Common.SpawnHanding
{
    public static class SpawnedObjectInvoker
    {
        private static readonly Type SUPPORTED_SPAWN_OBJECT_GENERIC_TYPE = typeof(ISpawnedObject<>);
        private const string ON_SPAWNED_METHOD_NAME = "OnSpawned";

        public static void InvokeOnSpawned(object spawnedObj, object dataObj)
        {
            Type spawnedObjType = spawnedObj.GetType();
            Type dataObjType = dataObj.GetType();
            
            if (VerifySpawnedObject(spawnedObjType, dataObjType))
            {
                CallOnSpawnedMethod(spawnedObj,spawnedObjType, dataObj);
            }
        }

        private static void CallOnSpawnedMethod(object spawnedObj,Type spawnedObjType, object dataObj)
        {
            MethodInfo method = spawnedObjType.GetMethod(ON_SPAWNED_METHOD_NAME);
            if (method != null)
            {
                method.Invoke(spawnedObj, new[] {dataObj});
            }
            else
            {
                Debug.LogError("Couldn't find method on spawned obj type: " + spawnedObjType);
            }
        }

        private static bool VerifySpawnedObject(Type spawnedObjType, Type dataObjType)
        {
            foreach (var spawnInterfaceType in spawnedObjType.GetInterfaces())
            {
                if(IsSupportedType(spawnInterfaceType) && HasGenericType(spawnInterfaceType, dataObjType))
                {
                    return true;
                }
            }

            Debug.LogError("The spawn object: " + spawnedObjType + " failed to verify. DataObj : " + dataObjType);
            return false;
        }

        private static bool HasGenericType(Type spawnedObjType, Type requiredType)
        {
            Type[] genericArguments = spawnedObjType.GetGenericArguments();

            return genericArguments.First() == requiredType;
        }

        private static bool IsSupportedType(Type objType)
        {
            return objType.IsGenericType && objType.GetGenericTypeDefinition() == SUPPORTED_SPAWN_OBJECT_GENERIC_TYPE;
        }
    }
}