using System.Collections.Generic;
using Common.UnitSystem.ExamplePlayer.Stats;
using Common.UnitSystem.Stats;
using Player;
using UnityEngine;

public class PlayerStatsManager : UnitStatsManager
{
    [SerializeField]
    private UnitHealthStats _healthStats;
        
    [SerializeField]
    private MovementStats _movementStats;

    [SerializeField] 
    private PlayerAttractedManager.Data _playerAttractedManagerData;

    public UnitHealthStats HealthStats => _healthStats;

    public MovementStats MovementStats => _movementStats;

    public PlayerAttractedManager.Data PlayerAttractedManagerData => _playerAttractedManagerData;

    protected override List<object> StatsEntries => new List<object>() { _healthStats, _movementStats };
}