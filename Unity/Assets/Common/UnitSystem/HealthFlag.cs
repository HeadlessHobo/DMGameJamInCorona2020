using System;

namespace Common.UnitSystem
{
    [Flags]
    public enum HealthFlag
    {
        None = 0,
        Destructable = 1,
        Killable = 2
    }
}