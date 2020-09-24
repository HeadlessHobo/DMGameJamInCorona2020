using System;
using Common.UnitSystem.ExamplePlayer.Stats;
using Common.UnitSystem.Stats;
using Common.UpgradeSystem.UpgradeStats.Unit;
using UnityEngine;

namespace Common.UpgradeSystem
{
    [Serializable]
    public class ExamplePlayerUpgrade
    {
        [SerializeField]
        private UnitMovementUpgradeStats _playerMovementUpgradeStats;
            
        [SerializeField]
        private UnitHealthUpgradeStats _playerHealthUpgradeStats;

        [SerializeField] 
        private UpgradeActionSelector[] _upgradeActionSelectors;

        public void ApplyUpgrade(ExamplePlayerStatsManager examplePlayerStatsManager)
        {
            ApplyUpgradeActions(examplePlayerStatsManager);
            ApplyUpgradeStats(examplePlayerStatsManager);
        }

        private void ApplyUpgradeActions(ExamplePlayerStatsManager examplePlayerStatsManager)
        {
            foreach (var upgradeActionSelector in _upgradeActionSelectors)
            {
                IUpgradeAction upgradeAction = upgradeActionSelector.CreateUpgradeAction<IUpgradeAction>();
                upgradeAction.DoUpgrade(examplePlayerStatsManager);
            }
        }

        private void ApplyUpgradeStats(ExamplePlayerStatsManager examplePlayerStatsManager)
        {
            _playerMovementUpgradeStats.ApplyUpgradeStats(examplePlayerStatsManager.GetStats<MovementStats>());
            _playerHealthUpgradeStats.ApplyUpgradeStats(examplePlayerStatsManager.GetStats<UnitHealthStats>());
        }
    }
}