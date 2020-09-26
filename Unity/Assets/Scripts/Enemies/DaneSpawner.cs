using System;
using Common;
using Common.SpawnHanding;
using Common.UnitSystem;
using Common.Util;
using Plugins.Timer.Source;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class DaneSpawner : MonoBehaviour
    {
        private const float DEGREES_IN_A_CIRLE = 360;
        
        [SerializeField]
        private BoxCollider2D _spawnArea;

        [SerializeField] 
        private DaneSpanerStatsManager _daneSpanerStatsManager;

        private bool _hasReachedMaxOrOver;
        private Data _data;

        private void Start()
        {
            _data = _daneSpanerStatsManager.DaneSpawnerData;
            SpawnLoop();
        }

        private void SpawnLoop()
        {
            UpdateMaxOrOverState();
            if(!_hasReachedMaxOrOver)
            {
                SpawnGroupOfDanes((int) Random.Range(_data.MinDanesPerSpawn.Value,
                    _data.MaxDanesPerSpawn.Value));
            }
            
            
            Timer.Register(_data.SpawnInterval.Value, SpawnLoop);
        }

        private void UpdateMaxOrOverState()
        {
            if (GameManager.Instance.DaneCount > _data.MaxDanes.Value)
            {
                _hasReachedMaxOrOver = true;
            }
            else if (GameManager.Instance.DaneCount <= _data.MinDanesForSpawningNewDanes.Value)
            {
                _hasReachedMaxOrOver = false;
            }
        }

        private void SpawnGroupOfDanes(int daneCount)
        {
            Vector2 daneCenterSpawnPoint = GetDaneSpawnPoint();
            float degreesPerDanes = DEGREES_IN_A_CIRLE / daneCount;
            for (int i = 0; i < daneCount; i++)
            {    
                Vector2 degreeAsVector = VectorUtil.DegreeToVector2(degreesPerDanes * i);

                GameObject spawnedDane = SpawnManager.Instance.Spawn(SpawnType.Dane,
                    daneCenterSpawnPoint + degreeAsVector * _data.DaneSpawnSize.Value);
                GameManager.Instance.AddDane(spawnedDane.GetComponentInParent<Dane>());
            }
        }

        private Vector2 GetDaneSpawnPoint()
        {
            Vector2 randomSpawnPoint = GetRandomSpawnPoint();
            while (ArePointWithinCamera(randomSpawnPoint))
            {
                randomSpawnPoint = GetRandomSpawnPoint();
            }

            return randomSpawnPoint;
        }

        private bool ArePointWithinCamera(Vector2 point)
        {
            Camera mainCamera = GameManager.Instance.MainCamera;

            Vector2 viewportPoint = mainCamera.WorldToViewportPoint(point);

            return viewportPoint.x > 0 && viewportPoint.x < 1 &&
                   viewportPoint.y > 0 && viewportPoint.y < 1;
        }

        private Vector2 GetRandomSpawnPoint()
        {
            var size = _spawnArea.size;
            var position = transform.position;
            float randomX = Random.Range(position.x - (size.x / 2), position.x + (size.x / 2));
            float randomY = Random.Range(position.y - (size.y / 2), position.y + (size.y / 2));
            
            return new Vector2(randomX, randomY);
        }
        
        [Serializable]
        public class Data
        {
            public Stat MaxDanes;
            public Stat MinDanesForSpawningNewDanes;
            public Stat MinDanesPerSpawn;
            public Stat MaxDanesPerSpawn;
            public Stat SpawnInterval;
            public Stat DaneSpawnSize;
        }
    }
}