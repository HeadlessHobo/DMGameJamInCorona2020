using System;
using Plugins.NaughtyAttributes.Scripts.Core.DrawerAttributes;
using UnityEngine;

namespace Common.SpawnHanding
{
    [Serializable]
    public class MonsterSpawnData
    {
        private string[] _spawnTypes = MonsterSpawnManager.SPAWN_TYPES;
    
        [SerializeField,Dropdown("_spawnTypes")]
        private string _monsterType;

        [SerializeField] 
        private GameObject _monsterPrefab;

        [SerializeField] 
        private float _spawnProcent;

        [SerializeField] 
        private int _maxSpawnCap = -1;

        public GameObject MonsterPrefab => _monsterPrefab;

        public float SpawnProcent => _spawnProcent;

        public int MaxSpawnCap => _maxSpawnCap;

        public string MonsterType => _monsterType;
    }
}
