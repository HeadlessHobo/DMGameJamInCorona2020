using System;
using System.Collections.Generic;
using Common.UnitSystem;
using Common.UnitSystem.Stats;
using UnityEngine;

[Serializable]
public class TNTStatsManager : UnitStatsManager
{
    [SerializeField]
    private TNT.Data _explosionData;

    public TNT.Data ExplosionData => _explosionData;
    
    protected override List<object> StatsEntries => new List<object> { _explosionData };
}