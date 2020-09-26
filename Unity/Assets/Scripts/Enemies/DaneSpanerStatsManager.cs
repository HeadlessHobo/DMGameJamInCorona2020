using System;
using System.Collections.Generic;
using Common.UnitSystem;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Enemies
{
    [Serializable]
    public class DaneSpanerStatsManager : UnitStatsManager
    {
        [SerializeField]
        private DaneSpawner.Data _daneSpawnerData;

        public DaneSpawner.Data DaneSpawnerData => _daneSpawnerData;
        
        protected override List<object> StatsEntries { get; }
    }
}