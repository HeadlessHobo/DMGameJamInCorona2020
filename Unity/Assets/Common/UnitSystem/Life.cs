using System;
using System.Collections;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Common.UnitSystem
{
    public delegate void Died(IUnit killedBy);
    public delegate void TookDamage(int damage, IUnit unitDealingDamage);

    public class Life
    {
        private bool _wasDeadLastFrame;
        private IUnit _killedByUnit;
        private IUnit _ownerUnit;
        private UnitHealthStats _unitHealthStats;
        private HealthFlag _healthFlags;

        public Stat Health
        {
            get { return _unitHealthStats.HealthStat; }
        }

        public bool IsDead => Health.Value <= 0;

        public event Died Died;
        public event TookDamage TookDamage;

        public Life(IUnit ownerUnit, UnitHealthStats unitHealthStats)
        {
            _ownerUnit = ownerUnit;
            _unitHealthStats = unitHealthStats;
        }

        public void TakeDamage(int damage, IUnit unitDealingDamage)
        {
            float healthBeforeTakingDamage = Health.Value;

            if (!IsDead)
            {
                Health.DecreaseTempStat(damage);
                TookDamage?.Invoke(damage, unitDealingDamage);
            }

            if (IsDead && healthBeforeTakingDamage > 0)
            {
                _killedByUnit = unitDealingDamage;
            }
        }

        public void Heal(int healAmount)
        {
            Health.IncreaseTempStat(healAmount);
        }

        public void Update()
        {
            if (!_wasDeadLastFrame && IsDead)
            {
                Die();
            }

            _wasDeadLastFrame = IsDead;
        }

        public void Die()
        {
            Health.DecreaseTempStat(99999);
            _wasDeadLastFrame = true;
            Died?.Invoke(_killedByUnit);
        }
    }
}