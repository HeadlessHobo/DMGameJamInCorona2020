using System.Collections.Generic;
using Common.UnitSystem;
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

    [SerializeField] 
    private Stat _throwingTNTStopTime;

    [SerializeField] 
    private Stat _blowingAwayActiveInTime;

    [SerializeField] 
    private Stat _quoteDelay;

    public UnitHealthStats HealthStats => _healthStats;

    public MovementStats MovementStats => _movementStats;

    public PlayerAttractedManager.Data PlayerAttractedManagerData => _playerAttractedManagerData;

    public Stat ThrowingTNTStopTime => _throwingTNTStopTime;

    public Stat BlowingAwayActiveInTime => _blowingAwayActiveInTime;

    public Stat QuoteDelay => _quoteDelay;

    protected override List<object> StatsEntries => new List<object>() { _healthStats, _movementStats };
}