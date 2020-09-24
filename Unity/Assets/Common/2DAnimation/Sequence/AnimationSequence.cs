using System;
using System.Collections.Generic;
using Common._2DAnimation.Abstract;
using UnityEngine;
using Animation = Common._2DAnimation.Abstract.Animation;

namespace Common._2DAnimation.Sequence
{
    [Serializable]
    public class AnimationSequence : Animation
    {
        private List<IAnimation> _animationsInSequence;
        private IAnimation _currentAnimationInSequence;
        private float _duration;

        public override Sprite CurrentFrame => _currentAnimationInSequence.CurrentFrame;

        public override float Duration
        {
            get
            {
                float duration = 0;

                foreach (var animationInSequence in _animationsInSequence)
                {
                    duration += animationInSequence.Duration;
                }

                return duration;
            }
        }

        public AnimationSequence(Animator animator, Data data) : base(data.Name, data.Speed, data.FramesPerSecond)
        {
            _currentFrameIndex = 0;
            LoadAnimations(animator, data.AnimationNamesInSequence);
        }

        protected override void OnPlay(bool looping)
        {
            _currentAnimationInSequence = _animationsInSequence[_currentFrameIndex];
            _currentAnimationInSequence.Play(false);
        }

        private void LoadAnimations(Animator animator, List<string> animationNamesInSequence)
        {
            _animationsInSequence = new List<IAnimation>();
            foreach (var animationNameInSequence in animationNamesInSequence)
            {
                _animationsInSequence.Add(animator.GetAnimation(animationNameInSequence));
            }
        }

        protected override void OnUpdate()
        {
            _currentAnimationInSequence.Speed = Speed;
            if (HasReachedEndOfAnimation())
            {
                OnReachedEndOfAnimation();
            }
            else if (!_currentAnimationInSequence.IsRunning)
            {
                MoveOnToNextAnimationInSequence();
            }
        }
        
        private bool HasReachedEndOfAnimation()
        {
            return _currentFrameIndex >=  _animationsInSequence.Count - 1;
        }

        private void MoveOnToNextAnimationInSequence()
        {
            _currentFrameIndex++;
            _currentAnimationInSequence = _animationsInSequence[_currentFrameIndex];
            _currentAnimationInSequence.Play(false);
        }
        
        [Serializable]
        public class Data
        {
            public string Name;
            public float FramesPerSecond;
            public float Speed;
            public List<string> AnimationNamesInSequence;
        }
    }
}