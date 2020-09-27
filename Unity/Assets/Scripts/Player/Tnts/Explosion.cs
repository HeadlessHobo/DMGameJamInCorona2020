using System;
using System.Collections.Generic;
using Common.SpawnHanding;
using Common.UnitSystem;
using Common.UnitSystem.Stats;
using Common.Util;
using Plugins.LeanTween.Framework;
using Plugins.Timer.Source;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class Explosion : Unit
{
    [SerializeField]
    private ExplosionStatsManager _explosionStatsManager;

    [SerializeField] 
    private Animator _explosionAnimator;
    
    [SerializeField]
    private CircleCollider2D _circleCollider2D;

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
            TriggerNotifier triggerNotifier = _circleCollider2D.gameObject.AddComponent<TriggerNotifier>();
            triggerNotifier.Init(new List<UnitType>(){ UnitType.All });
            triggerNotifier.UnitStayed += OnUnitEntered;

            _circleCollider2D.radius = _explosionData.ExplosionRadius.Value;
            _circleCollider2D.enabled = false;
            
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
        _circleCollider2D.enabled = true;
    }

    private void OnExplosionEnded()
    {
        _circleCollider2D.enabled = false;
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
            AnimationManager.Instance.QUIBlownAwayAni();
        }
        else if (unitType == UnitType.Enemy)
        {
            unit.GetArmor<IArmor>().Die();
        }
    }

    protected override void Update()
    {
        base.Update();
        if (Application.isEditor && !Application.isPlaying && _explosionStatsManager != null)
        {
            _circleCollider2D.radius = _explosionStatsManager.ExplosionData.ExplosionRadius.Value;
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