using System;

namespace Common.Menu.TransitionEffects.Abstract
{
    public interface ITransitionEffect
    {
        string TransitionEffectName { get; }
        
        void Apply(TransitionType transitionType, Action onFinishedTransition);
    }
    
    public abstract class TransitionEffectBase<TTransitionEffectData> : ITransitionEffect where TTransitionEffectData : TransitionEffectData
    {
        private Action _onFinishedTransition;
        
        protected TTransitionEffectData _transitionEffectData;

        public string TransitionEffectName => _transitionEffectData.TransitionEffectName;
        
        protected TransitionEffectBase(TTransitionEffectData transitionEffectData)
        {
            _transitionEffectData = transitionEffectData;
        }

        public void Apply(TransitionType transitionType, Action onFinishedTransition)
        {
            _onFinishedTransition = onFinishedTransition;
            Apply(transitionType);
        }

        protected void OnFinishedTransition()
        {
            _onFinishedTransition?.Invoke();
        }

        protected abstract void Apply(TransitionType transitionType);
    }
}