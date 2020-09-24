using UnityEngine;

namespace Common.UnitSystem.Stats
{
     public class UnitAttackStats: IResetStats
        {
            [SerializeField] 
            private Stat _attackSpeedStat;
            
            public float AttackSpeed => _attackSpeedStat.Value;
            
            public void IncreaseAttackSpeedRatio(float attackSpeedRatioIncrease)
            {
                _attackSpeedStat.DecreaseStat(attackSpeedRatioIncrease);
            }
    
            public virtual void ResetStats()
            {
                _attackSpeedStat.ResetTempStats();
            }
        }
}