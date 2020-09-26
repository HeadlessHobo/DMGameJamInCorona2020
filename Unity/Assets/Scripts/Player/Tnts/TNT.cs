using System;
using System.Collections.Generic;
using Common.SpawnHanding;
using Common.UnitSystem;
using Common.Util;
using Plugins.LeanTween.Framework;
using Plugins.Timer.Source;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class TNT : MonoBehaviour
{
    [FormerlySerializedAs("_explosionStatsManager"), SerializeField]
    private TNTStatsManager tntStatsManager;

    [SerializeField] 
    private CircleCollider2D _circleCollider2D;

    private bool _hasExploded;
    private TNT.Data _tntData;

    private void Awake()
    {
        if (Application.isPlaying)
        {
            _tntData = tntStatsManager.ExplosionData;
            Timer.Register(_tntData.ExplosionTimer.Value, SpawnExplosion);
        }
    }

    private void SpawnExplosion()
    {
        SpawnManager.Instance.Spawn<Explosion>(SpawnType.Explosion, transform.position, _tntData);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Application.isEditor && !Application.isPlaying && tntStatsManager != null)
        {
            _circleCollider2D.radius = tntStatsManager.ExplosionData.ExplosionRadius.Value;
        }
    }


    [Serializable]
    public class Data
    {
        public Stat ExplosionForce;
        public Stat ExplosionRadius;
        public Stat ExplosionTimer;
        public Stat ExplosionLiveTime;
        public Stat ExplosionExpandTime;
    }
}