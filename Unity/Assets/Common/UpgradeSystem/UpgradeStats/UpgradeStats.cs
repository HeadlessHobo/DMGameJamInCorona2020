using System;
using Common.UnitSystem.Stats;

namespace Common.UpgradeSystem.UpgradeStats
{
    public abstract class UpgradeStats<TStats> where TStats : class
    {
        public bool SupportsStatsType(Type statsType)
        {
            Type myStatsType = typeof(TStats);
            return statsType.IsAssignableFrom(typeof(TStats)) || myStatsType == statsType;
        }
        
        public void ApplyUpgradeStats(IUnitStatsManager unitStatsManager)
        {
            ApplyUpgradeStats(unitStatsManager.GetStats<TStats>());
        }
    
        public abstract void ApplyUpgradeStats(TStats stats);
        
    }
}