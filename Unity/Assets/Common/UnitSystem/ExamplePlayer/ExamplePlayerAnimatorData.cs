using System;
using Common._2DAnimation;
using Common._2DAnimation.State;
using UnityEngine;

namespace Common.UnitSystem.ExamplePlayer
{
    [Serializable]
    public class ExamplePlayerAnimatorData : AnimatorData
    {    
        [SerializeField]
        private ExamplePlayerAnimationStatesPropertiesContainer _examplePlayerAnimationStatesPropertiesContainer;
        
        public override IAnimationStatesPropertiesContainer AnimationStateProperties => _examplePlayerAnimationStatesPropertiesContainer;
    }
}