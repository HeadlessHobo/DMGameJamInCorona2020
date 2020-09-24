using Common._2DAnimation.State;
using Common.UnitSystem;
using Common.UnitSystem.ExamplePlayer;
using Common.UnitSystem.ExamplePlayer.Stats;
using Common.UnitSystem.LifeCycle;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Movement
{
    public abstract class Movement : IFixedUpdate
    {
        private Vector2 _currentMoveDirection;
        private Rigidbody2D _rigidbody2D;
        private MovementStats _movementStats;

        public Vector2 CurrentMoveDirection => _currentMoveDirection;

        protected Movement(UnitMovementSetup unitMovementSetup, MovementStats movementStats)
        {
            _rigidbody2D = unitMovementSetup.Rigidbody2D;
            _movementStats = movementStats;
        }
        
        public void FixedUpdate()
        {
            SetMoveDirection();

            if(Mathf.Abs(_currentMoveDirection.x) > 0 || Mathf.Abs(_currentMoveDirection.y) > 0)
            {
                _rigidbody2D.AddForce(_currentMoveDirection * _movementStats.Speed, ForceMode2D.Force);
            }
        }

        private void SetMoveDirection()
        {
            _currentMoveDirection = GetMoveDirection();
            _currentMoveDirection.Normalize();
        }

        protected abstract Vector2 GetMoveDirection();
    }

}