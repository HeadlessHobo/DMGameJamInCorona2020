using System;
using Common;
using Common.Movement;
using Common.UnitSystem;
using Common.UnitSystem.ExamplePlayer.Stats;
using UnityEngine;

namespace Enemies
{
    public class EnemiesMovement : Movement
    {
        private Vector2 _moveDirection;
        private EnemyMovementState _currentState;

        public EnemiesMovement(MovementStats movementStats, UnitMovementSetup unitMovementSetup, MovementType movementType) : base(movementStats, unitMovementSetup, movementType)
        {
            _currentState = EnemyMovementState.Stop;
        }

        public void SetNewState(EnemyMovementState state)
        {
            _currentState = state;
        }

        protected override Vector2 GetMoveDirection()
        {
            switch (_currentState)
            {
                case EnemyMovementState.Follow:
                    return GetDirectionToPlayer();
                case EnemyMovementState.Scared:
                    return GetDirectionAwayFromPlayer();
                case EnemyMovementState.Stop:
                    return Vector2.zero;
            }
            
            return Vector2.zero;
        }

        private Vector2 GetDirectionAwayFromPlayer()
        {
            return -GetDirectionToPlayer();
        }

        private Vector2 GetDirectionToPlayer()
        {
            return (GameManager.Instance.Player
                .GetSetup<UnitMovementSetup>().MovementTransform.position - _unitMovementSetup.MovementTransform.position).normalized;
        }
    }
}