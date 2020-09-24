using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.SpawnHanding
{
    [Serializable][CreateAssetMenu(fileName = "SpawnTypeToPrefab", menuName = "Mappings/Spawn type to prefab mappings", order = 50)]
    public class SpawnTypeToPrefabMapping : ScriptableObject
    {
        [SerializeField]
        private List<SpawnTypeToPrefabMappingData> _mappingList;

        private bool IsMultipleSpawnMappings()
        {
            foreach (var spawnType in Enum.GetValues(typeof(SpawnType)))
            {
                if (_mappingList != null && GetCountOfSpawnPositionType((SpawnType)spawnType) > 1)
                {
                    return true;
                }
            }

            return false;
        }

        private int GetCountOfSpawnPositionType(SpawnType spawnType)
        {
            int count = 0;
        
            foreach (var mapping in _mappingList)
            {
                if (mapping != null && mapping.SpawnType == spawnType)
                {
                    count++;
                }
            }

            return count;
        }

        public GameObject GetSpawnPrefabForSpawnType(SpawnType spawnType)
        {
            return _mappingList.Find(item => item.SpawnType == spawnType).Prefab;
        }
    }

    [Serializable]
    public class SpawnTypeToPrefabMappingData
    {
        public SpawnType SpawnType;
        public GameObject Prefab;
    }
}