using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.SpawnHanding
{
    public class MonsterSpawnManager : MonoBehaviour
    {
        public static readonly string[] SPAWN_TYPES = new[]
        {
            "UglySpawningItem",
            "TheMostOpSpawningUnit",
            "UselessSpawningUnit",
        };
    
        private List<int> _allSpawnPointIndexes;
    
        private Dictionary<string, int> _spawnTypesToSpawnCount;

        [SerializeField]
        private int _minMonstersToSpawn;

        [SerializeField] 
        private int _maxMonstersToSpawn;

        [SerializeField]
        private Transform _spawnPointsParent;

        [SerializeField] 
        private MonsterSpawnData[] _monsterSpawnDataEntries;

        private void Awake()
        {
            _allSpawnPointIndexes = new List<int>();
            _spawnTypesToSpawnCount = new Dictionary<string, int>();
            foreach (var monsterType in SPAWN_TYPES)
            {
                _spawnTypesToSpawnCount.Add(monsterType, 0);
            }
        }

        public void SpawnMonsters()
        {
            FillSpawnPointIndexes();
            ResetSpawnCount();
            int monstersToSpawn = Random.Range(_minMonstersToSpawn, _maxMonstersToSpawn);
            for (int i = 0; i < monstersToSpawn; i++)
            {
                SpawnRandomMonster();
            }
        }

        private void FillSpawnPointIndexes()
        {
            _allSpawnPointIndexes.Clear();
            for (int i = 0; i < _spawnPointsParent.childCount; i++)
            {
                _allSpawnPointIndexes.Add(i);
            }
        }

        private void SpawnRandomMonster()
        {
            GameObject randomMonsterPrefab = GetRandomMonsterPrefab();
            Instantiate(randomMonsterPrefab, GetRandomSpawnPoint());
        }

        private GameObject GetRandomMonsterPrefab()
        {
            float randomProcent = Random.Range(0, 100f);
            float currentProcent = 0;
            foreach (var monsterSpawnData in _monsterSpawnDataEntries)
            {
                currentProcent += monsterSpawnData.SpawnProcent;
                if (randomProcent <= currentProcent 
                    && (monsterSpawnData.MaxSpawnCap <= -1 || _spawnTypesToSpawnCount[monsterSpawnData.MonsterType] <= monsterSpawnData.MaxSpawnCap))
                {
                    _spawnTypesToSpawnCount[monsterSpawnData.MonsterType]++;
                    return monsterSpawnData.MonsterPrefab;
                }
            }

            return null;
        }

        private void ResetSpawnCount()
        {
            foreach (var monsterType in SPAWN_TYPES)
            {
                _spawnTypesToSpawnCount[monsterType] = 0;
            }
        }
    

        private Transform GetRandomSpawnPoint()
        {
            int randomSpawnPointIndex = _allSpawnPointIndexes[Random.Range(0, _allSpawnPointIndexes.Count)];
            _allSpawnPointIndexes.Remove(randomSpawnPointIndex);
            return _spawnPointsParent.GetChild(randomSpawnPointIndex);
        }
    }
}