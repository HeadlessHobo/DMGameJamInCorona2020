using System;
using System.Collections.Generic;
using Common.SpawnHanding;
using Common.UnitSystem;
using Common.UnitSystem.Stats;
using Common.Util;
using Player.Tnts;
using Plugins.LeanTween.Framework;
using Plugins.Timer.Source;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class TNT : Unit
{
    [SerializeField] 
    private TNTStatsManager _statsManager;

    [SerializeField] 
    private UnitSetup _unitSetup;

    public override UnitType UnitType => UnitType.TNT;
    protected override IUnitStatsManager StatsManager => _statsManager;
    protected override IArmor Armor { get; set; }
    protected override List<object> Setups => new List<object>() { _unitSetup };

    private void Start()
    {
        if (Application.isPlaying)
        {
            _statsManager = Instantiate(_statsManager);
            Armor = new UnitArmor(this, HealthFlag.Destructable, _unitSetup, new UnitHealthStats(new Stat(1), new Stat(0)));
            Timer.Register(_statsManager.TntData.ExplosionTimer.Value, SpawnExplosion);
            AddLifeCycleObjects(Armor);
        }
    }

    private void SpawnExplosion()
    {
        SpawnManager.Instance.Spawn(SpawnType.Explosion, transform.position);
        Armor.Die();
    }
    
    [Serializable]
    public class Data
    {
        public Stat ExplosionTimer;
    }
}