using UnityEngine;

namespace Common._2DAnimation.Abstract
{
    public interface IAnimation
    {
        string Name { get; }
        float FramesPerSecond { get; }
        float Speed { get; set; }
        Sprite CurrentFrame { get; }
        bool IsRunning { get; }
        float Duration { get; }

        event AnimationDone AnimationDone;
        
        void Play(bool looping);
        void Pause();
        void Stop();
        void Update();
        void ResetSpeed();
    }
}