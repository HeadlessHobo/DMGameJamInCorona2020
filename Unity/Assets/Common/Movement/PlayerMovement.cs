using Common.UnitSystem;
using Common.UnitSystem.ExamplePlayer.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Movement
{
    public class PlayerMovement : Movement
    {
        private const float CHANCE_FOR_SMOKING = 30f;
        
        private Vector2 _moveDirection;
        private bool _didMoveLastFrame;
        
        public bool CanMove { get; set; }
        
        public PlayerMovement(MovementStats movementStats, UnitMovementSetup unitMovementSetup, MovementType movementType) : 
            base(movementStats, unitMovementSetup, movementType)
        {
            CanMove = true;
        }

        public void OnPlayerMoved(Vector2 amountMoved)
        {
            if (CanMove)
            {
                _moveDirection = amountMoved;
                UpdateAnimations();
            }
        }

        private void UpdateAnimations()
        {
            if (_moveDirection.magnitude <= 0 && _didMoveLastFrame)
            {
                float randomProcent = Random.Range(0, 100);
                if (randomProcent <= CHANCE_FOR_SMOKING)
                {
                    AnimationManager.Instance.QUISmokeIdleAni();
                }
                else
                {
                    AnimationManager.Instance.QUIIdleAni();
                }

                _didMoveLastFrame = false;
            }
            
            if (_moveDirection.magnitude > 0 && !_didMoveLastFrame)
            {
                AnimationManager.Instance.QUIMoveAni();
                _didMoveLastFrame = true;
            }
        }

        protected override Vector2 GetMoveDirection()
        {
            if (!CanMove)
            {
                return Vector2.zero;
            }
            return _moveDirection;
        }      
    }
}