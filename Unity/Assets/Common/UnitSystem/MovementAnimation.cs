using System;
using Common._2DAnimation.State;
using Common.Movement;
using Common.UnitSystem.ExamplePlayer.Stats;
using Common.UnitSystem.LifeCycle;
using UnityEngine;

namespace Common.UnitSystem
{
    public class MovementAnimation : IUpdate
    {
        private AnimationStateManager _animationStateManager;
        private Rigidbody2D _rigidbody2D;
        private Func<Vector2> _getMoveDirection;
        private MovementStats _movementStats;

        public MovementAnimation(UnitMovementSetup unitMovementSetup, Func<Vector2> getMoveDirection, MovementStats movementStats, AnimationStateManager animationStateManager)
        {
            _rigidbody2D = unitMovementSetup.Rigidbody2D;
            _getMoveDirection = getMoveDirection;
            _movementStats = movementStats;
            _animationStateManager = animationStateManager;
        }

        public void Update()
        {
            UpdateAnimationState();
        }
        
        private void UpdateAnimationState()
        {
            _animationStateManager.UpdateDataObj<MovementAnimationState>(new MovementAnimationState.Data()
            {
                MoveDirection = _getMoveDirection(),
                CurrentSpeed = _rigidbody2D.velocity.magnitude,
                MaxSpeed = _movementStats.MaxSpeed,
            });
        }
    }
}