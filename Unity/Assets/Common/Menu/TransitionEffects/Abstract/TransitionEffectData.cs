using Common.Util.Pickers;
using UnityEngine;

namespace Common.Menu.TransitionEffects.Abstract
{
    public class TransitionEffectData
    {
        [SerializeField] 
        private string _transitionEffectName;

        public string TransitionEffectName => _transitionEffectName;
    }
}