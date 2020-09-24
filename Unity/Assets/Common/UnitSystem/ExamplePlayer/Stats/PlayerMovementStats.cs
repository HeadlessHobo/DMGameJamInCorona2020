using System;
using System.Collections.Generic;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Common.UnitSystem.ExamplePlayer.Stats
{
    [Serializable]
    public class MovementStats : UnitMovementStats
    {
        [SerializeField] 
        protected Stat _speed;

        [SerializeField] 
        protected Stat _maxSpeed;
        
        public float Speed => _speed.Value;

        public float MaxSpeed => _maxSpeed.Value;

        public override void DecreaseSpeedTempByProcent(float procentDecrease)
        {
            _speed.DecreaseTempStat(_speed.Value * (procentDecrease / 100));
        }

        public override void ResetStats()
        {
            _speed.ResetTempStats();
            _maxSpeed.ResetTempStats();
        }
        
        public void IncreaseSpeed(float speed)
        {
            _speed.IncreaseStat(speed);
        }
        
        public void IncreaseMaxSpeed(float increaseInSpeed)
        {
            _maxSpeed.IncreaseStat(increaseInSpeed);
        }
    }
}