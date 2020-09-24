using System;
using System.Collections.Generic;
using System.Linq;
using Common.UnitSystem.Stats;
using Plugins.Timer.Source;

namespace Common.UnitSystem
{
    public class UnitSlowManager
    {
        private List<SlowData> _slowsApplied;
        private UnitMovementStats _movementStats;

        public UnitSlowManager(UnitMovementStats movementStats)
        {
            _movementStats = movementStats;
            _slowsApplied = new List<SlowData>();
        }

        public void AddSlow(float slowProcent, float duration)
        {
            string slowId = Guid.NewGuid().ToString();
            
            _slowsApplied.Add(new SlowData(slowId, slowProcent));
            RemoveSlowAfterDuration(slowId, duration);
            SlowRemovedOrAdded();
        }

        private void RemoveSlowAfterDuration(string slowId, float duration)
        {
            Timer.Register(duration, () => RemoveSlowWithId(slowId));
        }

        private void RemoveSlowWithId(string slowId)
        {
            SlowData slow = _slowsApplied.Find(item => item.Id == slowId);
            _slowsApplied.Remove(slow);
            SlowRemovedOrAdded();
        }

        private void SlowRemovedOrAdded()
        {
            _movementStats.ResetStats();
            float strongestSlowProcent = GetStrongestSlow();
            _movementStats.DecreaseSpeedTempByProcent(strongestSlowProcent);
        }

        private float GetStrongestSlow()
        {
            return _slowsApplied.Max(item => item.SlowProcent);
        }
    }
}