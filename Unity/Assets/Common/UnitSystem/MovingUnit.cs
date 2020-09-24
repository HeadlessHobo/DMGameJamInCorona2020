namespace Common.UnitSystem
{
    public abstract class MovingUnit : Unit
    {
        protected abstract UnitSlowManager SlowManager { get; set; }

        public T GetSlowManager<T>() where T : UnitSlowManager
        {
            return ConvertObjectAndVerifyType<T>(SlowManager);
        }
    }
}