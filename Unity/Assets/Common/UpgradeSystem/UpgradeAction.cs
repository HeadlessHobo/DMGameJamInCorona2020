using System;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Common.UpgradeSystem
{
    public abstract class UpgradeAction<TStatsManager> : IUpgradeAction where TStatsManager : IUnitStatsManager
    {
        public void DoUpgrade(IUnitStatsManager statsManager)
        {
            if (CheckIfTypesMatch(statsManager))
            {
                DoUpgrade((TStatsManager)statsManager);
            }
            else
            {
                Debug.LogError("Types didn't match. Types were statsManager: " + statsManager.GetType());
            }
        }

        private bool CheckIfTypesMatch(IUnitStatsManager statsManager)
        {
            Type statsManagerType = statsManager.GetType();
            
            return statsManagerType.IsAssignableFrom(typeof(TStatsManager));
        }
        
        public abstract void DoUpgrade(TStatsManager statsManager);
    }
}