using System;
using System.Collections.Generic;
using Common._2DAnimation.Abstract;

namespace Common._2DAnimation.State
{
    public class IdleAnimationState : AnimationState
    {
        private const string IDLE_ANIMATION_NAME = "Idle";

        public override string[] RequiredAnimationNames => new[]
        {
            IDLE_ANIMATION_NAME
        };
        protected override bool LoopAnimation => true;

        public IdleAnimationState(Animator animator, AnimationStateProperties stateProperties) : base(animator, stateProperties)
        {
        }

        protected override IAnimation GetStartEnterStateAnimation(List<IAnimation> animations)
        {
            return animations.Find(item => item.Name == IDLE_ANIMATION_NAME);
        }

        public override bool CanTransitionTo()
        {
            return true;
        }
        
        [Serializable]
        public class Properties : AnimationStateProperties
        {
            public override AnimationState Create(Animator animator)
            {
                return new IdleAnimationState(animator, this);
            }
        }
    }
}