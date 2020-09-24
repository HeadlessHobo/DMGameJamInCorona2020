using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.UnitSystem.Stats
{
    [Serializable]
    public class UnitHealthStats : IResetStats
    {
        private List<Stat> _stats;
        
        [SerializeField] 
        private Stat _healthStat;

        [SerializeField] 
        private Stat _invulnerabilityDuration;

        public Stat InvulnerabilityDuration => _invulnerabilityDuration;

        public Stat HealthStat => _healthStat;

        public IEnumerable<Stat> AllStats => new[]
        {
            _healthStat, _invulnerabilityDuration
        };

        public UnitHealthStats(Stat healthStat, Stat invulnerabilityDuration)
        {
            
        }

        public void IncreaseHealth(int health)
        {
            _healthStat.IncreaseStat(health);
        }

        public virtual void ResetStats()
        {
            _healthStat.ResetTempStats();
        }
    }
}