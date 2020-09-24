using UnityEngine;

namespace Common
{
    public class GameObjectsManager
    {
        public const string MANAGERS_PREFAB_PATH = "Prefabs/Managers";
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnBeforeSceneLoadRuntimeMethod()
        {
            SpawnManagersPrefab();
        }

        private static void SpawnManagersPrefab()
        {
            GameObject managersPrefab = Resources.Load<GameObject>(MANAGERS_PREFAB_PATH);

            GameObject spawnedManagersGo = Object.Instantiate(managersPrefab);
            Object.DontDestroyOnLoad(spawnedManagersGo);
        }
    }
}