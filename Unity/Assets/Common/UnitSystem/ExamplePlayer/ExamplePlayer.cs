using System.Collections.Generic;
using Common.Movement;
using Common.UnitSystem.ExamplePlayer.Stats;
using Common.UnitSystem.Stats;
using UnityEngine;
using Animator = Common._2DAnimation.Animator;


namespace Common.UnitSystem.ExamplePlayer
{
    public class ExamplePlayer : MovingUnit
    {
        private Animator _playerAnimator;
        private Movement.Movement _movement;
        private MovementAnimation _movementAnimation;

        [SerializeField] 
        private UnitSetup _unitSetup;
        
        [SerializeField] 
        private UnitGraphicSetup _unitGraphicSetup;
        
        [SerializeField]
        private UnitMovementSetup _unitMovementSetup;

        [SerializeField] 
        private ExamplePlayerAnimatorData _examplePlayerAnimatorData;

        [SerializeField]
        private ExamplePlayerStatsManager _statsManager;

        public override UnitType UnitType => UnitType.Player;

        public Animator PlayerAnimator => _playerAnimator;

        protected override IUnitStatsManager StatsManager => _statsManager;

        protected override List<object> Setups => new List<object>() { _unitSetup, _unitGraphicSetup, _unitMovementSetup };
        
        protected override IArmor Armor { get; set; }
        protected override UnitSlowManager SlowManager { get; set; }

        protected  void Start()
        {
            base.Awake();
            SlowManager = new UnitSlowManager(GetStatsManager<ExamplePlayerStatsManager>().MovementStats);
            Armor = new UnitArmor(this, HealthFlag.Destructable | HealthFlag.Killable, _unitSetup, _statsManager.HealthStats);
            _playerAnimator = new Animator(this, _examplePlayerAnimatorData, _unitGraphicSetup);
            
            _movementAnimation = new MovementAnimation(_unitMovementSetup, () => _movement.CurrentMoveDirection, 
                _statsManager.GetStats<MovementStats>(), _playerAnimator.AnimationStateManager);
            AddLifeCycleObjects(_playerAnimator, Armor, _movement, _movementAnimation);
        }
    }
}