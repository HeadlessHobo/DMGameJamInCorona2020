using System;
using Common.UnitSystem.ExamplePlayer.Stats;
using UnityEngine;

namespace Common.UpgradeSystem.UpgradeStats.Unit
{
    [Serializable]
    public abstract class UnitMovementUpgradeStats<TStats> : UpgradeStats<TStats> where TStats : MovementStats 
    {
        [SerializeField]
        private float _maxMoveSpeed;
 
        public override void ApplyUpgradeStats(TStats stats)
        {
            stats.IncreaseSpeed(_maxMoveSpeed);
        }
    }

    [Serializable]
    public class UnitMovementUpgradeStats : UnitMovementUpgradeStats<MovementStats>
    {
        
    }
}