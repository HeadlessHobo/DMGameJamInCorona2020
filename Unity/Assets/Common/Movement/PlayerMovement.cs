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
        private HumanAniScript _humanAniScript;
        
        public bool CanMove { get; set; }
        
        public PlayerMovement(MovementStats movementStats, UnitMovementSetup unitMovementSetup, MovementType movementType, HumanAniScript humanAniScript) : 
            base(movementStats, unitMovementSetup, movementType)
        {
            _humanAniScript = humanAniScript;
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
            if (_moveDirection.magnitude <= 0)
            {
                if (_didMoveLastFrame)
                {
                    _humanAniScript.HumanIdleAni();
                    _didMoveLastFrame = false;
                }

                if (!AnimationManager.Instance.HasIdleAnimation)
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
                }
            }
            Debug.Log("UpdateAnimations");
            if (_moveDirection.magnitude > 0)
            {
                if (!AnimationManager.Instance.HasMovementAnimation)
                {
                    AnimationManager.Instance.QUIMoveAni();
                }
                
                if (!_didMoveLastFrame)
                {
                    _humanAniScript.HumanMove();
                    _didMoveLastFrame = true;
                }
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