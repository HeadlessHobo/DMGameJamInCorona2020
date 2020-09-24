using System;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Common.UpgradeSystem.UpgradeStats.Unit
{
    public abstract class UnitHealthUpgradeStats<TStats> : UpgradeStats<TStats> where TStats : UnitHealthStats
    {
        [SerializeField] private int _health;

        public override void ApplyUpgradeStats(TStats stats)
        {
            stats.IncreaseHealth(_health);
        }
    }
    
    [Serializable]
    public class UnitHealthUpgradeStats : UnitHealthUpgradeStats<UnitHealthStats>
    {
        
    }
}