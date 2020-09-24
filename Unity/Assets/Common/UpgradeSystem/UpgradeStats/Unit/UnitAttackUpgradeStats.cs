using System;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Common.UpgradeSystem.UpgradeStats.Unit
{
    public abstract class UnitAttackUpgradeStats<TStats> : UpgradeStats<TStats> where TStats : UnitAttackStats
    {
        [SerializeField] private float _attackSpeed;

        public override void ApplyUpgradeStats(TStats stats)
        {
            stats.IncreaseAttackSpeedRatio(_attackSpeed);
        }
    }
    
    [Serializable]
    public class UnitAttackUpgradeStats : UnitHealthUpgradeStats<UnitHealthStats>
    {
        
    }
}