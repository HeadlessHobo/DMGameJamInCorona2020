using System;
using System.Collections.Generic;
using Common.Movement;
using Common.UnitSystem;
using Common.UnitSystem.ExamplePlayer.Stats;
using Common.UnitSystem.Stats;
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
        
        private EnemiesMovement _enemiesMovement;
        private DaneState _currentState;

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
                _enemiesMovement = new EnemiesMovement(_daneStatsManager.GetStats<MovementStats>(), _unitMovementSetup, MovementType.Transform);
                AddLifeCycleObjects(Armor, _enemiesMovement);
            }
        }

        public void SetNewState(DaneState newState)
        {
            DaneState oldState = _currentState;
            _currentState = newState;
            if (oldState != newState)
            {
                SwitchToNewState(newState, oldState);
            }
        }

        protected override void EditorUpdate()
        {
            base.EditorUpdate();
        }

        protected override void Update()
        {
            base.Update();
            DebugPanel.Log("State", "Dane", _currentState);
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
                Timer.Register(interval, () => StartColorloop(spriteRenderer, colorIndex + 1, interval, colors));
            }
        }
    }
}