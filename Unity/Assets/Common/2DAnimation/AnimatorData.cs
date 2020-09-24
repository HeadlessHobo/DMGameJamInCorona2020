using System;
using System.Collections.Generic;
using Common._2DAnimation.Abstract;
using Common._2DAnimation.Sequence;
using Common._2DAnimation.SpriteSheet;
using Common._2DAnimation.State;
using UnityEngine;

namespace Common._2DAnimation
{
    [Serializable]
    public abstract class AnimatorData
    {
        [SerializeField]
        private List<AnimationCreator> _animationCreators;

        [SerializeField] 
        private List<AnimationSequence.Data> _animationSequenceDataEntries;

        public List<AnimationCreator> AnimationCreators => _animationCreators;

        public abstract IAnimationStatesPropertiesContainer AnimationStateProperties { get; }

        public List<IAnimation> GetAnimationsFromAnimationSequenceDataEntries(Animator animator)
        {
            if (_animationSequenceDataEntries != null)
            {
                return GetAnimations(animator);
            }
            
            return new List<IAnimation>();
        }
        
        public List<IAnimation> GetAnimations(Animator animator)
        {
            List<IAnimation> animations = new List<IAnimation>();
            
            foreach (var animationSequenceData in _animationSequenceDataEntries)
            {
                animations.Add(new AnimationSequence(animator, animationSequenceData));
            }

            return animations;
        } 
    }
}