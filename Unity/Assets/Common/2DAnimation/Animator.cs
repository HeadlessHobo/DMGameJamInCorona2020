using System.Collections.Generic;
using Common._2DAnimation.Abstract;
using Common._2DAnimation.SpriteSheet;
using Common._2DAnimation.State;
using Common.UnitSystem;
using Common.UnitSystem.LifeCycle;
using UnityEngine;

namespace Common._2DAnimation
{
    public delegate void AnimationStarted(IAnimation animation);
    
    public delegate void AnimationDone(IAnimation animation);
    
    public class Animator : IUpdate
    {
        private List<IAnimation> _animations;
        private bool _shouldUpdateAnimationStateManager;
        private IUnit _unit;
        private AnimatorData _animatorData;
        private AnimationStateManager _animationStateManager;
        private SpriteRenderer _spriteRenderer;

        public AnimationStateManager AnimationStateManager => _animationStateManager;

        public event AnimationStarted AnimationStarted;
        public event AnimationDone AnimationDone;

        public Animator(IUnit unit, AnimatorData animatorData, UnitGraphicSetup unitGraphicSetup)
        {
            _unit = unit;
            _spriteRenderer = unitGraphicSetup.SpriteRenderer;
            _animations = new List<IAnimation>();
            _animatorData = animatorData;
            AddAnimationsFromAnimatorCreators();
            _animations.AddRange(animatorData.GetAnimationsFromAnimationSequenceDataEntries(this));
            _shouldUpdateAnimationStateManager = true;
            _animationStateManager = new AnimationStateManager(animatorData.AnimationStateProperties.GetAllAnimationStateDataEntires().ToArray(), this);
        }

        private void AddAnimationsFromAnimatorCreators()
        {
            foreach (var animationCreator in _animatorData.AnimationCreators)
            {
                _animations.Add(animationCreator.CreateAnimation());
            }
        }

        public void AddAnimation(IAnimation animation)
        {
            _animations.Add(animation);
        }

        public void PlayAnimation(string name, bool looping = false)
        {
            PlayAnimation(GetAnimation(name), looping);
        }

        public void PlayAnimation(IAnimation animation, bool looping = false)
        {
            StopAllAnimations();
            _shouldUpdateAnimationStateManager = false;
            animation.Play(looping);
            animation.AnimationDone += OnAnimationDone;
            AnimationStarted?.Invoke(animation);
        }

        private void OnAnimationDone(IAnimation animation)
        {
            animation.AnimationDone -= OnAnimationDone;
            AnimationDone?.Invoke(animation);
            _shouldUpdateAnimationStateManager = true;
        }

        private void StopAllAnimations()
        {
            foreach (var animation in _animations)
            {
                animation.Stop();
            }
            _animationStateManager.StopAllAnimationStates();
        } 
        
        public void PauseAnimation(string name)
        {
            IAnimation animation = GetAnimation(name);
            animation.Pause();
        }

        public void StopAnimation(string name)
        {
            IAnimation animation = GetAnimation(name);
            animation.Stop();
        }

        public IAnimation GetAnimation(string name)
        {
            return _animations.Find(item => item.Name == name);
        }

        public void Update()
        {
            foreach (var animation in _animations)
            {
                animation.Update();
                if (animation.IsRunning)
                {
                    _spriteRenderer.sprite = animation.CurrentFrame;
                }
            }

            bool? isDead = _unit.GetArmor<IArmor>()?.IsDead;
            if (_shouldUpdateAnimationStateManager && !isDead.GetValueOrDefault())
            {
                _animationStateManager.UpdateStates(); 
            }
        }
    }
}