using System.Collections.Generic;
using Common.UnitSystem.ExamplePlayer.Stats;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Enemies
{
    public class DaneStatsManager : UnitStatsManager
    {
        [SerializeField]
        private UnitHealthStats _healthStats;
        
        [SerializeField]
        private MovementStats _movementStats;

        public UnitHealthStats HealthStats => _healthStats;

        public MovementStats MovementStats => _movementStats;

        protected override List<object> StatsEntries => new List<object>() { _healthStats, _movementStats };
    }
}