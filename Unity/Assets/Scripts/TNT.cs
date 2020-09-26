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
    private TNT.Data _explosionData;

    private void Awake()
    {
        if (Application.isPlaying)
        {
            _explosionData = tntStatsManager.ExplosionData;
            TriggerNotifier triggerNotifier = gameObject.AddComponent<TriggerNotifier>();
            triggerNotifier.Init(new List<UnitType>(){ UnitType.All });
            triggerNotifier.UnitEntered += OnUnitEntered;
            Timer.Register(_explosionData.ExplosionTimer.Value, OnStartExplosion);
            Timer.Register(_explosionData.ExplosionTimer.Value + _explosionData.ExplosionLiveTime.Value, OnEndExplosion);
        }
    }

    private void OnStartExplosion()
    {
        _hasExploded = true;

        gameObject.LeanScale(transform.localScale * _explosionData.ExplosionRadius.Value,
            _explosionData.ExplosionExpandTime.Value);
    }

    private void OnEndExplosion()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Application.isEditor && !Application.isPlaying && tntStatsManager != null)
        {
            _circleCollider2D.radius = tntStatsManager.ExplosionData.ExplosionRadius.Value;
        }
    }

    private void OnUnitEntered(UnitType unitType, IUnit unit)
    {
        if (_hasExploded)
        {
            OnUnitHitByExplosion(unitType, unit);
        }
    }

    private void OnUnitHitByExplosion(UnitType unitType, IUnit unit)
    {
        if (unitType == UnitType.Player)
        {
            unit.GetSetup<UnitMovementSetup>().Rigidbody2D.
                AddExplosionForce(tntStatsManager.ExplosionData.ExplosionForce.Value, transform.position, tntStatsManager.ExplosionData.ExplosionRadius.Value);
        }
        else if (unitType == UnitType.Enemy)
        {
            unit.GetArmor<IArmor>().Die();
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