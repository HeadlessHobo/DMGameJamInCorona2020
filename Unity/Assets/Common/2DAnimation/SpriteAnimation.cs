using UnityEngine;
using Animation = Common._2DAnimation.Abstract.Animation;

namespace Common._2DAnimation
{
    public class SpriteAnimation : Animation
    {
        private Sprite[] _sprites;
        private float _nextAnimationFrameTime;
        private float _duration;

        public override Sprite CurrentFrame => _sprites[_currentFrameIndex];

        public override float Duration => (_sprites.Length / FramesPerSecond) * Speed;

        public SpriteAnimation(string name, float framesPerSecond, float speed, Sprite[] sprites) : base(name, framesPerSecond, speed)
        {
            _sprites = sprites;
        }
        
        protected override void OnPlay(bool looping)
        {
            SetNextAnimationFrameTime();
        }
        
        protected override void OnUpdate()
        {
            if (HasReachedNextFrameTime())
            {
                SetNextAnimationFrameTime();
                UpdateAnimationFrameIndex();
            }
        }

        private bool HasReachedNextFrameTime()
        {
            return _nextAnimationFrameTime <= Time.time;
        }

        private void UpdateAnimationFrameIndex()
        {
            _currentFrameIndex++;
            
            if (HasReachedEndOfAnimation())
            {
                OnReachedEndOfAnimation();
            }
        }

        private bool HasReachedEndOfAnimation()
        {
            return _currentFrameIndex >= _sprites.Length;
        }

        private void SetNextAnimationFrameTime()
        {
            _nextAnimationFrameTime = Time.time + (1 / FramesPerSecond) * Speed;
        }
    }
}