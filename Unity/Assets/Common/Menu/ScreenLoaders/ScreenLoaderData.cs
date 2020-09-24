using Common.Extensions;
using Common.Util.Buttons;
using Common.Util.Pickers;
using UnityEngine;

namespace Common.Menu.ScreenLoaders
{
    public abstract class ScreenLoaderData
    {
        [SerializeField]
        private string _screenName;

        [SerializeField] 
        private TransitionEffectPicker _inTransition;
        
        [SerializeField] 
        private TransitionEffectPicker _outTransition;

        public string ScreenName => _screenName;

        public string InTransitionEffectName => _inTransition.PickedValue;
        public string OutTransitionEffectName => _outTransition.PickedValue;
    }
}