using Common.UnitSystem.Stats;

namespace Common.UpgradeSystem
{
    public interface IUpgradeAction 
    {
        void DoUpgrade(IUnitStatsManager statsManager);
    }
}