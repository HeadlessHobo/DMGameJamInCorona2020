using System;
using UnityEngine;

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
        private float _minStatValue = int.MinValue;
        
        [SerializeField]
        private float _maxStatValue = -1;

        public float MinStatValue
        {
            get => _minStatValue;
            set => _minStatValue = value;
        }
        
        public float MaxStatValue
        {
            get => _maxStatValue;
            set => _maxStatValue = value;
        }
        
        public float CurrentProcent => Value / _maxStatValue;

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
            _statChange -= ReturnStatChangeValueWithinLimits(-statDecrease);
        }
        
        public void IncreaseTempStat(float statIncrease)
        {
            _tempStatChange += ReturnStatChangeValueWithinLimits(statIncrease);
        }

        public void DecreaseTempStat(float statDecrease)
        {
            _tempStatChange -= ReturnStatChangeValueWithinLimits(-statDecrease);
        }

        private float ReturnStatChangeValueWithinLimits(float stateChangeValue)
        {
            float newValue = Value + stateChangeValue;
               
               if (MinStatValue > int.MinValue && newValue < MinStatValue)
               {
                   return MinStatValue - Value;
               }
               else if (MaxStatValue > -1 && newValue > MaxStatValue)
               {
                   return MaxStatValue - Value;
               }
   
               return stateChangeValue;
        }

        public void ResetTempStats()
        {
            _tempStatChange = 0;
        }
    }
}