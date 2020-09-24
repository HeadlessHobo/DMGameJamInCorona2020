using System.Collections.Generic;

namespace Common.UnitSystem.Stats
{
    public abstract class UnitMovementStats : IResetStats
    {
        public abstract void DecreaseSpeedTempByProcent(float procentDecrease);
        public abstract void ResetStats();
    }
}