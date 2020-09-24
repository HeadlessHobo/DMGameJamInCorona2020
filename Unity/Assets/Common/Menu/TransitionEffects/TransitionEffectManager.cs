using System;
using System.Collections.Generic;
using Common.Menu.TransitionEffects.Abstract;
using Common.Menu.TransitionEffects.Fade;
using UnityEngine;

namespace Common.Menu.TransitionEffects
{
    public class TransitionEffectManager : MonoBehaviour
    {
        private List<ITransitionEffect> _transitionEffects;
        
        [SerializeField]
        private TransitionEffectsData _transitionEffectsData;

        private void Awake()
        {
            CreateTransitionEffects();
        }

        private void CreateTransitionEffects()
        {
            _transitionEffects = new List<ITransitionEffect>();
            foreach (var fadeTransitionEffectData in _transitionEffectsData.FadeTransitionEffectDataEntries)
            {
                _transitionEffects.Add(new FadeTransitionEffect(fadeTransitionEffectData));
            }
        }

        public void ApplyTransitionEffect(string transitionEffectName, TransitionType transitionType, Action onTransitionFinished = null)
        {
            if (transitionEffectName != TransitionEffectsData.NONE_EFFECT_NAME)
            {
                _transitionEffects.Find(item => item.TransitionEffectName == transitionEffectName).Apply(transitionType, onTransitionFinished);
            }
            else
            {
                onTransitionFinished?.Invoke();
            }
        }
    }
}