using System;
using System.Collections.Generic;
using Common.UnitSystem;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Player.Tnts
{
    [Serializable]
    public class TNTStatsManager : UnitStatsManager
    {
        [SerializeField]
        private TNT.Data _tntData;

        public TNT.Data TntData => _tntData;
        
        protected override List<object> StatsEntries => new List<object>() { _tntData };
    }
}