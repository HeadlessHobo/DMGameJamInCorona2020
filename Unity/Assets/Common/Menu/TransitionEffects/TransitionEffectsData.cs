using System;
using System.Collections.Generic;
using Common.Menu.TransitionEffects.Fade;
using UnityEngine;

namespace Common.Menu.TransitionEffects
{
    [Serializable]
    public class TransitionEffectsData : ScriptableObject
    {
        public const string NONE_EFFECT_NAME = "None";
        
        [SerializeField]
        private List<FadeTransitionEffectData> _fadeTransitions;

        public List<FadeTransitionEffectData> FadeTransitionEffectDataEntries => _fadeTransitions;

        public List<string> GetAllTransitionEffectNames()
        {
            List<string> allTransitionEffectNames = new List<string>();
            allTransitionEffectNames.Add(NONE_EFFECT_NAME);

            foreach (var fadeTransitionEffect in _fadeTransitions)
            {
                allTransitionEffectNames.Add(fadeTransitionEffect.TransitionEffectName);
            }

            return allTransitionEffectNames;
        }
    }
}