using UnityEngine;

namespace Common._2DAnimation.State
{
    public abstract class AnimationStateProperties
    {
        [SerializeField]
        private int _priority;

        public int Priority => _priority;

        public abstract AnimationState Create(Animator animator);
    }
}