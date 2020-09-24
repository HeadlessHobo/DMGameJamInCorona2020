using UnityEngine;

namespace Common._2DAnimation.Abstract
{
    public abstract class Animation : IAnimation
    {
        private bool _isRunning;
        private float _startSpeed;
        
        protected bool _looping;
        protected int _currentFrameIndex;

        public string Name { get; }
        public float FramesPerSecond { get; }

        public float Speed { get; set; }
        public bool IsRunning => _isRunning;

        public abstract Sprite CurrentFrame { get; }
        public abstract float Duration { get; }
        
        public event AnimationDone AnimationDone;

        protected Animation(string name, float framesPerSecond, float speed)
        {
            FramesPerSecond = framesPerSecond;
            Name = name;
            Speed = speed;
            _startSpeed = speed;
        }

        public void Play(bool looping)
        {
            _currentFrameIndex = 0;
            _isRunning = true;
            _looping = looping;
            OnPlay(looping);
        }

        public void Pause()
        {
            _isRunning = false;
            OnPause();
        }

        public void Stop()
        {
            _currentFrameIndex = 0;
            _isRunning = false;
            _looping = false;
            AnimationDone?.Invoke(this); 
            OnStop();
        }

        public void Update()
        {
            if (_isRunning)
            {
                OnUpdate();
            }
        }

        public void ResetSpeed()
        {
            Speed = _startSpeed;
        }

        protected abstract void OnUpdate();


        protected virtual void OnPlay(bool looping)
        {
            
        }
        
        protected virtual void OnPause()
        {
            
        }
        
        protected virtual void OnStop()
        {
            
        }
        
        protected void OnReachedEndOfAnimation()
        {
            _currentFrameIndex = 0;
            if (!_looping)
            {
                Stop();
            }
        }

        public override string ToString()
        {
            return $"Name: {Name} | IsRuning: {IsRunning} Speed: {Speed} Duration: {Duration}";
        }
    }
}