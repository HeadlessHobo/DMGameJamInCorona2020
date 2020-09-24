using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common._2DAnimation.State
{
    [Serializable]
    public class ExamplePlayerAnimationStatesPropertiesContainer : IAnimationStatesPropertiesContainer
    {
        [SerializeField] 
        private IdleAnimationState.Properties _idleAnimationStateProperties;
        
        [SerializeField] 
        private MovementAnimationState.Properties _movementAnimationStateProperties;

        public List<AnimationStateProperties> GetAllAnimationStateDataEntires()
        {
            return new List<AnimationStateProperties>()
            {
                _idleAnimationStateProperties,
                _movementAnimationStateProperties
            };
        }
    }
}