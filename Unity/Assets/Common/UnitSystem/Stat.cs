using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.UnitSystem
{
    [Serializable]
    public class Stat
    {
        private float _statChange;
        private float _tempStatChange;
        
        [SerializeField] 
        private float _startStat;

        [SerializeField] 
        private bool _usesMinMax;
        
        [SerializeField]
        private float _minAllowedStatValue = int.MinValue;
        
        [SerializeField]
        private float _maxAllowedStatValue = -1;

        public float MinAllowedStatValue
        {
            get => _minAllowedStatValue;
            set => _minAllowedStatValue = value;
        }
        
        public float MaxAllowedStatValue
        {
            get => _maxAllowedStatValue;
            set => _maxAllowedStatValue = value;
        }
        
        public float CurrentProcent => Value / _maxAllowedStatValue;

        public float Value
        {
            get { return _startStat + _statChange + _tempStatChange; }
        }

        public Stat(float startValue)
        {
            _startStat = startValue;
        }
        
        public void IncreaseStat(float statIncrease)
        {
            _statChange += ReturnStatChangeValueWithinLimits(statIncrease);
        }

        public void DecreaseStat(float statDecrease)
        {
            _statChange += ReturnStatChangeValueWithinLimits(-statDecrease);
        }
        
        public void IncreaseTempStat(float statIncrease)
        {
            _tempStatChange += ReturnStatChangeValueWithinLimits(statIncrease);
        }

        public void DecreaseTempStat(float statDecrease)
        {
            _tempStatChange += ReturnStatChangeValueWithinLimits(-statDecrease);
        }

        private float ReturnStatChangeValueWithinLimits(float statChangeValue)
        {
            float newValue = Value + statChangeValue;
            
            if (ShouldUseMinAllowedStatValue() && newValue < MinAllowedStatValue)
            {
                float statChangeFromCurrentValueToMinAllowedValue = MinAllowedStatValue - Value;
                return statChangeFromCurrentValueToMinAllowedValue;
            }
            else if (ShouldUseMaxAllowedStatValue() && newValue > MaxAllowedStatValue)
            {
                float statChangeFromCurrentValueToMaxAllowedValue = MaxAllowedStatValue - Value;
                return statChangeFromCurrentValueToMaxAllowedValue;
            }

            return statChangeValue;
        }

        private bool ShouldUseMinAllowedStatValue()
        {
            return MinAllowedStatValue > int.MinValue;
        }
        
        private bool ShouldUseMaxAllowedStatValue()
        {
            return MaxAllowedStatValue > -1;
        }

        public void ResetTempStats()
        {
            _tempStatChange = 0;
        }
    }
}