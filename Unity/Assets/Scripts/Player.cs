using System.Collections.Generic;
using Common.Movement;
using Common.SpawnHanding;
using Common.UnitSystem;
using Common.UnitSystem.ExamplePlayer.Stats;
using Common.UnitSystem.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MovingUnit
{
    private PlayerMovement _movement;

    [SerializeField] 
    private UnitSetup _unitSetup;
        
    [SerializeField] 
    private UnitGraphicSetup _unitGraphicSetup;
        
    [SerializeField]
    private UnitMovementSetup _unitMovementSetup;

    [SerializeField]
    private PlayerStatsManager _statsManager;

    public override UnitType UnitType => UnitType.Player;
        
    protected override IUnitStatsManager StatsManager => _statsManager;

    protected override List<object> Setups => new List<object>() { _unitSetup, _unitGraphicSetup, _unitMovementSetup };
        
    protected override IArmor Armor { get; set; }
    protected override UnitSlowManager SlowManager { get; set; }

    protected  void Start()
    {
        base.Awake();
        SlowManager = new UnitSlowManager(GetStatsManager<PlayerStatsManager>().MovementStats);
        Armor = new UnitArmor(this, HealthFlag.Destructable | HealthFlag.Killable, _unitSetup, _statsManager.HealthStats);
        _movement = new PlayerMovement(_statsManager.GetStats<MovementStats>(), _unitMovementSetup, MovementType.Rigidbody);
        AddLifeCycleObjects(Armor, _movement);
    }

    public void OnMove(InputValue inputValue)
    {
        _movement.OnPlayerMoved(inputValue.Get<Vector2>());
    }

    public void OnFireTnT(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            SpawnManager.Instance.Spawn(SpawnType.Explosion, _unitMovementSetup.MovementTransform.position);
        }
    }
}