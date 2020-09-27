using System;
using Common.Menu.TransitionEffects.Abstract;
using Common.UI;
using Plugins.Timer.Source;

namespace Common.Menu.TransitionEffects.Fade
{
    public class FadeTransitionEffect : TransitionEffectBase<FadeTransitionEffectData>
    {
        private const float MAX_ALPHA = 1;
        private const float MIN_ALPHA = 0;
        
        public FadeTransitionEffect(FadeTransitionEffectData transitionEffectData) : base(transitionEffectData)
        {
        }

        protected override void Apply(TransitionType transitionType)
        {
            switch (transitionType)
            {
                case TransitionType.In:
                    FadeManager.Instance.FadeTo(MAX_ALPHA, 0);
                    
                    FadeManager.Instance.FadeTo(MIN_ALPHA, _transitionEffectData.FadeInTime);
                    Timer.Register(_transitionEffectData.FadeInTime, OnFinishedTransition);
                    break;
                case TransitionType.Out:
                    FadeManager.Instance.FadeTo(MIN_ALPHA, 0);
                    
                    FadeManager.Instance.FadeTo(MAX_ALPHA, _transitionEffectData.FadeOutTime);
                    Timer.Register(_transitionEffectData.FadeOutTime, OnFinishedTransition);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(transitionType), transitionType, null);
            }
        }

        public override void Remove()
        {
            FadeManager.Instance.FadeTo(MIN_ALPHA, 0);
        }
    }
}