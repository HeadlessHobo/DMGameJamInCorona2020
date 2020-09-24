namespace Common.UnitSystem.Stats
{
    public interface IUnitStatsManager : IResetStats
    {
        T GetStats<T>(bool logError = true) where T : class;

        bool TryGetStats<T>(out T stats) where T : class;
    }
}