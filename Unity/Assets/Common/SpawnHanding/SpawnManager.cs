using System;
using System.Collections.Generic;
using System.Linq;
using Common.SpawnHanding;
using Gamelogic.Extensions;
using UnityEngine;
using Object = UnityEngine.Object;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField]
    private SpawnTypeToPrefabMapping spawnTypeToPrefabMapping;

    private List<SpawnPoint> _spawnPoints
    {
        get
        {
            if (_spawnPointsSource == null)
            {
                _spawnPointsSource = FindObjectsOfType<SpawnPoint>().ToList();
            }

            return _spawnPointsSource;
        }
    }
    private List<SpawnPoint> _spawnPointsSource;

    public void Spawn<T>(SpawnType spawnType, Action<T> onSpawned = null) where T : MonoBehaviour
    {
        T spawnedObject = Spawn<T>(GetSpawnPointFromSpawnType(spawnType));
        
        onSpawned?.Invoke(spawnedObject);
    }

    public void SpawnAllWithType(SpawnType spawnType)
    {
        GameObject spawnPrefab = GetSpawnPrefabForSpawnType(spawnType);
        foreach (var spawnPoint in _spawnPoints)
        {
            if (spawnPoint.SpawnType == spawnType)
            {
                Spawn<MonoBehaviour>(spawnPoint);
            }
        }
    }

    public GameObject Spawn(SpawnPoint spawnPoint)
    {
        GameObject spawnPrefab = GetSpawnPrefabForSpawnType(spawnPoint.SpawnType);
        GameObject spawnedGo = Instantiate(spawnPrefab, spawnPoint.Position, spawnPoint.Rotation);
        spawnedGo.transform.localScale = spawnPoint.Scale;
        return spawnedGo;
    }

    public GameObject Spawn(SpawnType spawnType, Vector2 spawnPosition)
    {
        GameObject spawnPrefab = GetSpawnPrefabForSpawnType(spawnType);
        return Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);
    }

    public T Spawn<T>(SpawnType spawnType, Vector2 spawnPosition, object dataObj) where T : Object
    {
        T spawnPrefab = GetSpawnPrefabForSpawnType(spawnType).GetComponent<T>();
        return Spawner.Spawn(spawnPrefab, spawnPosition, Vector3.zero, dataObj);
    }

    public GameObject GetSpawnPrefabForSpawnType(SpawnType spawnType)
    {
        return spawnTypeToPrefabMapping.GetSpawnPrefabForSpawnType(spawnType);
    }
    
    public T GetSpawnPrefabForSpawnType<T>(SpawnType spawnType)
    {
        return spawnTypeToPrefabMapping.GetSpawnPrefabForSpawnType(spawnType).GetComponent<T>();
    }

    private T Spawn<T>(SpawnPoint spawnPoint)
    {
        return Spawn(spawnPoint).GetComponent<T>();
    }

    private SpawnPoint GetSpawnPointFromSpawnType(SpawnType spawnType)
    {
        return _spawnPoints.Find(item => item.SpawnType == spawnType);
    }
}