using System;
using Common.Menu.TransitionEffects.Abstract;
using UnityEngine;

namespace Common.Menu.TransitionEffects.Fade
{
    [Serializable]
    public class FadeTransitionEffectData : TransitionEffectData
    {
        [SerializeField]
        private float _fadeInTime;

        [SerializeField] 
        private float _fadeOutTime;

        public float FadeInTime => _fadeInTime;

        public float FadeOutTime => _fadeOutTime;
    }
}