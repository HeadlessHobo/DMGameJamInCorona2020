using System;
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
        private MovementStats _movementStats;
        private MovementType _movementType;
        
        protected UnitMovementSetup _unitMovementSetup;

        public Vector2 CurrentMoveDirection => _currentMoveDirection;

        protected Movement(MovementStats movementStats, UnitMovementSetup unitMovementSetup, MovementType movementType)
        {
            _unitMovementSetup = unitMovementSetup;
            _movementStats = movementStats;
            _movementType = movementType;
        }
        
        public void FixedUpdate()
        {
            SetMoveDirection();
            Move();
        }

        private void Move()
        {
            switch (_movementType)
            {
                case MovementType.Rigidbody:
                    MoveViaRigidbody();
                    break;
                case MovementType.Transform:
                    MoveViaTransform();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void MoveViaTransform()
        {
            _unitMovementSetup.MovementTransform.Translate(_currentMoveDirection * (_movementStats.Speed * Time.deltaTime));
        }

        private void MoveViaRigidbody()
        {
            if(Mathf.Abs(_currentMoveDirection.x) > 0 || Mathf.Abs(_currentMoveDirection.y) > 0)
            {
                _unitMovementSetup.Rigidbody2D.AddForce(_currentMoveDirection * _movementStats.Speed, ForceMode2D.Force);
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