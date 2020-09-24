using System;
using System.Collections.Generic;
using System.Linq;
using Common.UnitSystem.LifeCycle;
using Common.UnitSystem.Stats;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Common.UnitSystem
{
    public delegate void KilledUnit(IUnit unitKilled);
    
    public class UnitArmor : IArmor, IUpdate
    {
        private IUnit _ownerUnit;
        private float _nextDamageableTime;
        private bool _wasDeadInLastFrame;
        private Life _life;
        private UnitHealthStats _unitHealthStats;
        private UnitSetup _unitSetup;
        private List<Func<bool>> _destroyRequirements;
        public event Died Died;
        public event TookDamage TookDamage;
        public event KilledUnit KilledUnit;

        public HealthFlag HealthFlags { get; }
        public bool IsDead => _life.IsDead;
        public bool IsInvulnerable => _nextDamageableTime >= Time.time;

        public UnitArmor(IUnit ownerUnit, UnitSetup unitSetup) : 
            this(ownerUnit, HealthFlag.None, unitSetup, new UnitHealthStats(new Stat(999), new Stat(0)))
        {
            
        }

        public UnitArmor(IUnit ownerUnit, HealthFlag healthFlags, UnitSetup unitSetup, UnitHealthStats healthStats)
        {
            _unitSetup = unitSetup;
            _ownerUnit = ownerUnit;
            HealthFlags = healthFlags;
            _unitHealthStats = healthStats;
            _life = new Life(ownerUnit, _unitHealthStats);
            _life.Died += OnDied;
            _life.TookDamage += (damage, unitDealingDamage) => TookDamage?.Invoke(damage, unitDealingDamage);
        }

        public void SetDeathRequirements(params Func<bool>[] destroyRequirements)
        {
            _destroyRequirements = destroyRequirements.ToList();
        }

        public void AddDestroyRequirement(Func<bool> destroyRequirement)
        {
            _destroyRequirements.Add(destroyRequirement);
        }

        public void TakeDamage(int damage, IUnit unitDealingDamage)
        {
            if (CanTakeDamage())
            {
                _life.TakeDamage(damage, unitDealingDamage);
                MakeInvulnerable();
            }
        }

        public void Die()
        {
            _life.Die();
        }

        private void OnDied(IUnit killedBy)
        {
            Died?.Invoke(killedBy);
            killedBy?.GetArmor<IArmor>().OnKilledUnit(_ownerUnit);
        }

        private bool HasCompletedAllDeathRequirements()
        {
            foreach (var deathRequirement in _destroyRequirements)
            {
                if (!deathRequirement())
                {
                    return false;
                }
            }

            return true;
        }

        public void OnKilledUnit(IUnit unitKilled)
        {
            KilledUnit?.Invoke(unitKilled);   
        }

        public void MakeInvulnerable()
        {
            _nextDamageableTime = Time.time + _unitHealthStats.InvulnerabilityDuration.Value;
        }

        public void Update()
        {
            _life.Update();
            if (_life.IsDead && HealthFlags.HasFlag(HealthFlag.Destructable) && HasCompletedAllDeathRequirements())
            {
                Object.Destroy(_unitSetup.RootGo);
            }
        }

        private bool CanTakeDamage()
        {
            return HealthFlags.HasFlag(HealthFlag.Killable) && !IsInvulnerable;
        }
    }
}