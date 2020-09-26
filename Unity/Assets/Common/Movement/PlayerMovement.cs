using Common.UnitSystem;
using Common.UnitSystem.ExamplePlayer.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Movement
{
    public class PlayerMovement : Movement
    {
        private Vector2 _moveDirection;
        
        public PlayerMovement(MovementStats movementStats, UnitMovementSetup unitMovementSetup, MovementType movementType) : 
            base(movementStats, unitMovementSetup, movementType)
        {
        }

        public void OnPlayerMoved(Vector2 amountMoved)
        {
            _moveDirection = amountMoved;
        }

        protected override Vector2 GetMoveDirection()
        {
            return _moveDirection;
        }      
    }
}