using System;
using System.Collections.Generic;
using Common.UnitSystem;
using Common.UnitSystem.Stats;
using Common.Util;
using Plugins.Timer.Source;
using UnityEngine;

[ExecuteInEditMode]
public class Explosion : Unit
{
    [SerializeField]
    private ExplosionStatsManager _explosionStatsManager;

    [SerializeField] 
    private Animator _explosionAnimator;
    
    [SerializeField]
    private Collider2D _collider2D;

    [SerializeField] 
    private UnitSetup _unitSetup;
    
    private Data _explosionData;

    public override UnitType UnitType => UnitType.Explosion;
    protected override IUnitStatsManager StatsManager => _explosionStatsManager;
    protected override IArmor Armor { get; set; }
    protected override List<object> Setups => new List<object>() { _unitSetup };

    private void Start()
    {
        if (Application.isPlaying)
        {
            _explosionStatsManager = Instantiate(_explosionStatsManager);
            _explosionData = _explosionStatsManager.ExplosionData;
            TriggerNotifier triggerNotifier = _collider2D.gameObject.AddComponent<TriggerNotifier>();
            triggerNotifier.Init(new List<UnitType>(){ UnitType.All });
            triggerNotifier.UnitStayed += OnUnitEntered;
            
            _collider2D.enabled = false;
            
            Armor = new UnitArmor(this, HealthFlag.Destructable, _unitSetup, new UnitHealthStats(new Stat(1), new Stat(0)));
            AddLifeCycleObjects(Armor);

            _explosionAnimator.Play("Explosion");
            Timer.Register(_explosionData.ExplosionAnimationWaitTime.Value, OnExplosionStarted);
            Timer.Register(_explosionData.ExplosionAnimationWaitTime.Value + _explosionData.ExplosionDamageAndPushTime.Value, OnExplosionEnded);
            Timer.Register(_explosionData.ExplosionLiveTime.Value, Armor.Die);
        }
    }

    private void OnExplosionStarted()
    {
        _collider2D.enabled = true;
    }

    private void OnExplosionEnded()
    {
        _collider2D.enabled = false;
    }
    
    private void OnUnitEntered(UnitType unitType, IUnit unit)
    {
        OnUnitHitByExplosion(unitType, unit);
    }

    private void OnUnitHitByExplosion(UnitType unitType, IUnit unit)
    {
        if (unitType == UnitType.Player)
        {
            unit.GetSetup<UnitMovementSetup>().Rigidbody2D.
                AddExplosionForce(_explosionData.ExplosionForce.Value, transform.position, _explosionData.ExplosionRadius.Value);
            (unit as PlayerManager)?.HitByExplosion();
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
        public Stat ExplosionLiveTime;
        public Stat ExplosionDamageAndPushTime;
        public Stat ExplosionAnimationWaitTime;
    }
}