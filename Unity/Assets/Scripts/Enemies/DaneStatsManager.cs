using System.Collections.Generic;
using Common.UnitSystem;
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

        [SerializeField] 
        private Stat _scaredTriggerRadius;

        [SerializeField] 
        private Stat _scaredRunTime;

        [SerializeField] 
        private Stat _deathTime;

        public UnitHealthStats HealthStats => _healthStats;

        public MovementStats MovementStats => _movementStats;

        public Stat ScaredTriggerRadius => _scaredTriggerRadius;

        public Stat ScaredRunTime => _scaredRunTime;

        public Stat DeathTime => _deathTime;

        protected override List<object> StatsEntries => new List<object>() { _healthStats, _movementStats };
    }
}