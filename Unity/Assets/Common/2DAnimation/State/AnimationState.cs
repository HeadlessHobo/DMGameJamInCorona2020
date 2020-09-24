using System.Collections.Generic;
using Common._2DAnimation.Abstract;
using UnityEngine;

namespace Common._2DAnimation.State
{
    public abstract class AnimationState : IAnimationState
    {
        private List<IAnimation> _animations;
        protected Animator _animator;

        public int Priority { get; }
        public abstract string[] RequiredAnimationNames { get; }
        protected abstract bool LoopAnimation { get; }

        protected AnimationState(Animator animator, AnimationStateProperties stateProperties)
        {
            _animator = animator;
            Priority = stateProperties.Priority;
            _animations = new List<IAnimation>();
            CheckIfAnimatorHasAllRequiredAnimations();
            foreach (var requiredAnimationName in RequiredAnimationNames)
            {
                _animations.Add(_animator.GetAnimation(requiredAnimationName));
            }
        }

        private void CheckIfAnimatorHasAllRequiredAnimations()
        {
            foreach (var requiredAnimation in RequiredAnimationNames)
            {
                if (_animator.GetAnimation(requiredAnimation) == null)
                {
                    Debug.LogError("Animator doesn't have all needed animations");
                }
            }
        }

        public abstract bool CanTransitionTo();

        public void EnterState()
        {
            IAnimation animation = GetStartEnterStateAnimation(_animations);
            animation.Play(LoopAnimation);
            OnEnterState(_animations); 
        }
        
        protected abstract IAnimation GetStartEnterStateAnimation(List<IAnimation> animations);
        
        public void Update()
        {
            OnUpdate(_animations);
        }
        
        public virtual void LeaveState()
        {
            foreach (var animation in _animations)
            {
                animation.Stop();
            }
            OnLeaveState(_animations);
        }
        
        public virtual bool SupportsDataObj(object dataObj)
        {
            return false;
        }

        public virtual void UpdateDataObj(object dataObj)
        {
        }

        protected virtual void OnUpdate(List<IAnimation> animations){ }

        protected virtual void OnEnterState(List<IAnimation> animations){ }
        
        protected virtual void OnLeaveState(List<IAnimation> animations){ }
    }

    public abstract class AnimationState<TProperties, TData> : AnimationState where TData : class where TProperties : AnimationStateProperties
    {
        protected TProperties _properties;
        protected TData _data;
        
        protected AnimationState(Animator animator, TProperties properties) : base(animator, properties)
        {
            _properties = properties;
        }

        public override bool SupportsDataObj(object dataObj)
        {
            return dataObj.GetType() == typeof(TData);
        }

        public  override void UpdateDataObj(object dataObj)
        {
            _data = dataObj as TData;
            OnUpdateDataObj(_data);
        }

        protected virtual void OnUpdateDataObj(TData data)
        {
        }
    }
}