using System;

namespace Common.Menu.TransitionEffects.Abstract
{
    public interface ITransitionEffect
    {
        string TransitionEffectName { get; }
        
        void Apply(TransitionType transitionType, Action onFinishedTransition);
        void Remove();
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

        public abstract void Remove();

        protected void OnFinishedTransition()
        {
            _onFinishedTransition?.Invoke();
        }

        protected abstract void Apply(TransitionType transitionType);
        
    }
}