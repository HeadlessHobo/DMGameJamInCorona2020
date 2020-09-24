using System.Collections.Generic;
using UnityEngine;

namespace Common.UnitSystem.Stats
{
    public abstract class UnitStatsManager<THealthStats> : ScriptableObject, IUnitStatsManager where THealthStats : UnitHealthStats
    {
        protected abstract List<object> StatsEntries { get; }
        
        protected void AddStats(object statsObj)
        {
            StatsEntries.Add(statsObj);
        }

        public void ResetStats()
        {
            foreach (var statsEntry in StatsEntries)
            {
                if (statsEntry is IResetStats resetStats)
                {
                    resetStats.ResetStats();
                }
            }
        }

        public T GetStats<T>(bool logError = true) where T : class 
        {
            foreach (var statsEntry in StatsEntries)
            {
                if (statsEntry is T stats)
                {
                    return stats;
                }
            }

            if (logError)
            {
                Debug.LogError("Couldn't find type:  " + typeof(T));
            }
            
            return null;
        }

        public bool TryGetStats<T>(out T stats) where T : class
        {
            stats = GetStats<T>(false);
            return stats != null;
        }
    }
}