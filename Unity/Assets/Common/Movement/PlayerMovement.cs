using Common.UnitSystem;
using Common.UnitSystem.ExamplePlayer.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Movement
{
    public class PlayerMovement : Movement
    {
        private PlayerInput _playerInput;
        private PlayerInputActions _playerInputActions;
        private Vector2 _moveDirection;
        
        public PlayerMovement(UnitMovementSetup unitMovementSetup, MovementStats movementStats, PlayerInputActions playerInputActions) : base(unitMovementSetup, movementStats)
        {
            _playerInput = unitMovementSetup.PlayerInput;
            _playerInputActions = playerInputActions;
            _playerInput.onActionTriggered += PlayerInputOnonActionTriggered;
        }

        private void PlayerInputOnonActionTriggered(InputAction.CallbackContext obj)
        {
            _moveDirection = Vector2.zero;
            if (obj.performed && obj.action.name == _playerInputActions.Player.Move.name)
            {
                _moveDirection = obj.ReadValue<Vector2>();
            }
        }

        protected override Vector2 GetMoveDirection()
        {
            //_playerInput.actions[_playerInputActions.Player.Move.name].ReadValue<Vector2>()
            return _moveDirection;
        }
    }
}