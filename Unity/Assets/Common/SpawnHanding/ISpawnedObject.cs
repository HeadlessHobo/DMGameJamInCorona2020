namespace Common.SpawnHanding
{
    public interface ISpawnedObject<TData>
    {
        void OnSpawned(TData data);
    }
}