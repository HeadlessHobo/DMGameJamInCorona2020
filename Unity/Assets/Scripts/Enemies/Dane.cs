using System;
using System.Collections.Generic;
using Common;
using Common.Movement;
using Common.UnitSystem;
using Common.UnitSystem.ExamplePlayer.Stats;
using Common.UnitSystem.Stats;
using Common.Util;
using Plugins.Timer.Source;
using UnityEngine;
using Yurowm.DebugTools;

namespace Enemies
{
    public class Dane : MovingUnit
    {
        [SerializeField]
        private DaneStatsManager _daneStatsManager;

        [SerializeField] 
        private UnitSetup _unitSetup;

        [SerializeField] 
        private UnitGraphicSetup _unitGraphicSetup;

        [SerializeField] 
        private UnitMovementSetup _unitMovementSetup;

        [SerializeField] 
        private GameObject _scaredTriggerGo;

        [SerializeField] 
        private HumanAniScript _humanAniScript;
        
        private EnemiesMovement _enemiesMovement;
        private DaneState _currentState;
        private bool _hasInitialized;
        private Timer _scaredUICountDownTimer;
        private bool _canDie;
        private bool _isDead;
        public override UnitType UnitType => UnitType.Enemy;
        protected override IUnitStatsManager StatsManager => _daneStatsManager;
        protected override IArmor Armor { get; set; }
        protected override List<object> Setups => new List<object>() { _unitSetup, _unitGraphicSetup, _unitMovementSetup };
        protected override UnitSlowManager SlowManager { get; set; }

        protected override void Awake()
        {
            base.Awake();
            if (Application.isPlaying)
            {
                _daneStatsManager = Instantiate(_daneStatsManager);
                Armor = new UnitArmor(this, HealthFlag.Destructable | HealthFlag.Killable, _unitSetup,
                    _daneStatsManager.HealthStats);
                Armor.AddDestroyRequirement(() => _canDie);
                Armor.Died += OnDied;
            }
        }

        private void OnDied(IUnit killedby)
        {
            _humanAniScript.HumanDeath();
            _enemiesMovement.CanMove = false;
            _isDead = true;
            Timer.Register(_daneStatsManager.DeathTime.Value,() => _canDie = true);
        }

        protected void Start()
        {
            if (Application.isPlaying)
            {
                SlowManager = new UnitSlowManager(GetStatsManager<DaneStatsManager>().MovementStats);
                _enemiesMovement = new EnemiesMovement(_daneStatsManager.GetStats<MovementStats>(), _unitMovementSetup, MovementType.Rigidbody);
                Destroy(_scaredTriggerGo);
                AddLifeCycleObjects(Armor, _enemiesMovement);
                _hasInitialized = true;
                GameManager.Instance.DaneDied += OnDaneDied;
            }
        }

        private void OnDaneDied(Dane daneDied)
        {
            Transform daneDiedTransform = daneDied.GetSetup<UnitMovementSetup>().MovementTransform;
            
            if (daneDiedTransform != null && _unitMovementSetup.MovementTransform != null && Vector2.Distance(daneDied.GetSetup<UnitMovementSetup>().MovementTransform.position,
                _unitMovementSetup.MovementTransform.position) < _daneStatsManager.ScaredTriggerRadius.Value)
            {
                SetNewState(DaneState.Scared);
            }
        }

        private void SetupScaredTrigger()
        {
            CircleCollider2D circleCollider2D = _scaredTriggerGo.GetComponentInChildren<CircleCollider2D>();
            circleCollider2D.radius = _daneStatsManager.ScaredTriggerRadius.Value;
        }

        public void SetNewState(DaneState newState)
        {
            if (_hasInitialized && !_isDead)
            {
                DaneState oldState = _currentState;
                _currentState = newState;
                if (oldState != newState)
                {
                    SwitchToNewState(newState, oldState);
                }
            }
        }

        private void SwitchToNewState(DaneState newState, DaneState oldState)
        {
            switch (newState)
            {
                case DaneState.Standing:
                    _enemiesMovement.SetNewState(EnemyMovementState.Stop);
                    _humanAniScript.HumanIdleAni();
                    break;
                case DaneState.Cheer:
                    _enemiesMovement.SetNewState(EnemyMovementState.Stop);
                    _humanAniScript.HumanExcitedAni();
                    break;
                case DaneState.Attracted:
                    _enemiesMovement.SetNewState(EnemyMovementState.Follow);
                    _humanAniScript.HumanMove();
                    break;
                case DaneState.Scared:
                    _enemiesMovement.SetNewState(EnemyMovementState.Scared);
                    _scaredUICountDownTimer = Timer.Register(_daneStatsManager.ScaredRunTime.Value, () => SetNewState(DaneState.Standing));
                    _humanAniScript.HumanMove();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Timer.Cancel(_scaredUICountDownTimer);
        }

        protected override void EditorUpdate()
        {
            base.EditorUpdate();
            SetupScaredTrigger();
        }
    }
}