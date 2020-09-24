using UnityEngine;

namespace Common.SpawnHanding
{
    public static class Spawner
    {
        private const string DEFAULT_FORMAT_FOR_SPAWNED_GO = "{0}:{1}";
        
        public static T Spawn<T>(T prefab, Vector2 position, Vector3 eulerAngles, object dataObj) where T : Object
        {
            T spawnedObj = Object.Instantiate(prefab, position, Quaternion.Euler(eulerAngles));
            SpawnedObjectInvoker.InvokeOnSpawned(spawnedObj, dataObj);

            return spawnedObj;
        }

        public static T SpawnWithFormattedNaming<T>(T prefab, Vector2 position, Vector3 eulerAngles, int index, object dataObj,
            string formatForSpawnedGo = DEFAULT_FORMAT_FOR_SPAWNED_GO) where T : Object
        {
            T spawnedObj = Spawn(prefab, position, eulerAngles, dataObj);
            SetNameOnSpawnedObject(spawnedObj, index, formatForSpawnedGo);

            return spawnedObj;
        }
        
        public static T Spawn<T>(T prefab, Transform parent, object dataObj) where T : Object
        {
            T spawnedObj = Object.Instantiate(prefab, parent);
            SpawnedObjectInvoker.InvokeOnSpawned(spawnedObj, dataObj);

            return spawnedObj;
        }

        private static void SetNameOnSpawnedObject(Object spawnedObj, int index, string formatForSpawnedGo)
        {
            spawnedObj.name = string.Format(formatForSpawnedGo, spawnedObj.name, index);
        }
    }
}