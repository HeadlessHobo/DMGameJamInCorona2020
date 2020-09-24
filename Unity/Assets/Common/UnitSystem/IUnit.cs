using Common.UnitSystem.Stats;

namespace Common.UnitSystem
{
    public interface IUnit
    {
        UnitType UnitType { get; }

        T GetArmor<T>() where T : IArmor;
        
        T GetStatsManager<T>() where T : IUnitStatsManager;
    }
}