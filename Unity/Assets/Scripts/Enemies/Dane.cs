using System;
using System.Collections.Generic;
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
        
        private EnemiesMovement _enemiesMovement;
        private DaneState _currentState;
        private bool _hasInitialized;
        private Timer _scaredTimer;
        private Timer _colorTimer;

        public override UnitType UnitType => UnitType.Enemy;
        protected override IUnitStatsManager StatsManager => _daneStatsManager;
        protected override IArmor Armor { get; set; }
        protected override List<object> Setups => new List<object>() { _unitSetup, _unitGraphicSetup, _unitMovementSetup };
        protected override UnitSlowManager SlowManager { get; set; }

        protected void Start()
        {
            if (Application.isPlaying)
            {
                _daneStatsManager = Instantiate(_daneStatsManager);
                SlowManager = new UnitSlowManager(GetStatsManager<DaneStatsManager>().MovementStats);
                Armor = new UnitArmor(this, HealthFlag.Destructable | HealthFlag.Killable, _unitSetup, _daneStatsManager.HealthStats);
                _enemiesMovement = new EnemiesMovement(_daneStatsManager.GetStats<MovementStats>(), _unitMovementSetup, MovementType.Rigidbody);
                SetupScaredTrigger();
                SetupScaredTriggerNotifier();
                AddLifeCycleObjects(Armor, _enemiesMovement);
                _hasInitialized = true;
            }
        }

        private void SetupScaredTrigger()
        {
            CircleCollider2D circleCollider2D = _scaredTriggerGo.GetComponentInChildren<CircleCollider2D>();
            circleCollider2D.radius = _daneStatsManager.ScaredTriggerRadius.Value;
        }
        
        private void SetupScaredTriggerNotifier()
        {
            TriggerNotifier triggerNotifier = _scaredTriggerGo.AddComponent<TriggerNotifier>();
            triggerNotifier.Init(new List<UnitType>() {UnitType.Explosion});
            triggerNotifier.UnitEntered += OnExplosionEntered;
        }

        private void OnExplosionEntered(UnitType unitType, IUnit unit)
        {
            SetNewState(DaneState.Scared);
        }

        public void SetNewState(DaneState newState)
        {
            if (_hasInitialized)
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
                    _unitGraphicSetup.SpriteRenderer.color = Color.blue;
                    break;
                case DaneState.Cheer:
                    _enemiesMovement.SetNewState(EnemyMovementState.Stop);
                    StartColorloop(_unitGraphicSetup.SpriteRenderer, 0, 0.25f, Color.yellow, Color.red);
                    break;
                case DaneState.Attracted:
                    _enemiesMovement.SetNewState(EnemyMovementState.Follow);
                    _unitGraphicSetup.SpriteRenderer.color = Color.red;
                    break;
                case DaneState.Scared:
                    _enemiesMovement.SetNewState(EnemyMovementState.Scared);
                    _unitGraphicSetup.SpriteRenderer.color = Color.black;
                    _scaredTimer = Timer.Register(_daneStatsManager.ScaredRunTime.Value, () => SetNewState(DaneState.Standing));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private void StartColorloop(SpriteRenderer spriteRenderer, int colorIndex, float interval, params Color[] colors)
        {
            if (colorIndex >= colors.Length)
            {
                colorIndex = 0;
            }

            if (spriteRenderer != null)
            {
                spriteRenderer.color = colors[colorIndex];
            }
            

            if (_currentState == DaneState.Cheer)
            {
                _colorTimer = Timer.Register(interval, () => StartColorloop(spriteRenderer, colorIndex + 1, interval, colors));
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Timer.Cancel(_scaredTimer);
            Timer.Cancel(_colorTimer);
        }

        protected override void EditorUpdate()
        {
            base.EditorUpdate();
            SetupScaredTrigger();
        }
    }
}