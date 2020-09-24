namespace Common.UnitSystem
{
    public class SlowData
    {
        public string Id { get; }
        public float SlowProcent { get; }
        
        public SlowData(string id, float slowProcent)
        {
            Id = id;
            SlowProcent = slowProcent;
        }
    }
}