using System;
using System.Collections.Generic;
using Common._2DAnimation.Abstract;
using UnityEngine;
using Yurowm.DebugTools;

namespace Common._2DAnimation.State
{
    public class MovementAnimationState : AnimationState<MovementAnimationState.Properties, MovementAnimationState.Data>
    {
        private const string WALK_LEFT_ANIMATION_NAME = "WalkLeft";
        private const string WALK_RIGHT_ANIMATION_NAME = "WalkRight";
        private const string WALK_UP_ANIMATION_NAME = "WalkUp";
        private const string WALK_DOWN_ANIMATION_NAME = "WalkDown";
        public override string[] RequiredAnimationNames => new[]
        {
            WALK_LEFT_ANIMATION_NAME,
            WALK_RIGHT_ANIMATION_NAME,
            WALK_UP_ANIMATION_NAME,
            WALK_DOWN_ANIMATION_NAME
        };
        
        protected override bool LoopAnimation => true;

        public MovementAnimationState(Animator animator,  Properties properties) : base(animator, properties)
        {
            
        }

        protected override IAnimation GetStartEnterStateAnimation(List<IAnimation> animations)
        {
            return GetWalkAnimation(animations);
        }

        public override bool CanTransitionTo()
        {
            return _data?.CurrentSpeed > _properties.MinAnimationSpeed;
        }

        protected override void OnLeaveState(List<IAnimation> animations)
        {
            foreach (var animation in animations)
            {
                animation.ResetSpeed();
            }
        }

        protected override void OnUpdate(List<IAnimation> animations)
        {
            IAnimation correctWalkAnimation = GetWalkAnimation(animations);
            if (!correctWalkAnimation.IsRunning)
            {
                _animator.PlayAnimation(correctWalkAnimation);
            }
            correctWalkAnimation.Speed = GetAnimationSpeed();
        }

        private IAnimation GetWalkAnimation(List<IAnimation> animations)
        {
            DebugPanel.Log("Movedirection", "Animation", _data.MoveDirection);
            Vector2 moveDirection = _data.MoveDirection;
            if (Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.y))
            {
                if (moveDirection.x > 0)
                {
                    return animations.Find(item => item.Name == WALK_RIGHT_ANIMATION_NAME);
                }
                else if (moveDirection.x < 0)
                {
                    return animations.Find(item => item.Name == WALK_LEFT_ANIMATION_NAME);
                }
            }

            if (Mathf.Abs(moveDirection.y) >= Mathf.Abs(moveDirection.x))
            {
                if (moveDirection.y > 0)
                {
                    return animations.Find(item => item.Name == WALK_UP_ANIMATION_NAME);
                }
                else if (moveDirection.y < 0)
                {
                    return animations.Find(item => item.Name == WALK_DOWN_ANIMATION_NAME);
                }
            }

            return animations[0];
        }

        private float GetAnimationSpeed()
        {
            float currentSpeedProcent = _data.CurrentSpeed / _data.MaxSpeed;

            return Math.Max(currentSpeedProcent * _properties.MaxAnimationSpeed, _properties.MinAnimationSpeed);
        }
        
        [Serializable]
        public class Properties : AnimationStateProperties
        {
            [SerializeField]
            private float _minSpeedRequired;
            
            [SerializeField]
            private float _minAnimationSpeed;
            
            [SerializeField]
            private float _maxAnimationSpeed;

            public float MinSpeedRequired => _minSpeedRequired;

            public float MinAnimationSpeed => _minAnimationSpeed;

            public float MaxAnimationSpeed => _maxAnimationSpeed;
            
            public override AnimationState Create(Animator animator)
            {
                return new MovementAnimationState(animator, this);
            }
        }
        
        public class Data
        {
            public Vector2 MoveDirection { get; set; }
            public Vector2 InputDirection { get; set; }
            public float CurrentSpeed { get; set; }
            public float MaxSpeed { get; set; }
        }
    }
}