using System;
using System.Collections.Generic;
using Common.UnitSystem;
using Common.UnitSystem.Stats;
using UnityEngine;

[Serializable]
public class ExplosionStatsManager : UnitStatsManager
{
    [SerializeField]
    private Explosion.Data _explosionData;

    public Explosion.Data ExplosionData => _explosionData;
    
    protected override List<object> StatsEntries => new List<object> { _explosionData };
}