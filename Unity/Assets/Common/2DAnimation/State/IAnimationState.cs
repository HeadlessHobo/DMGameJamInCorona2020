namespace Common._2DAnimation.State
{
    public interface IAnimationState
    {
        int Priority { get; }
        string[] RequiredAnimationNames { get; }
        bool SupportsDataObj(object dataObj);
        void UpdateDataObj(object dataObj);
        bool CanTransitionTo();
        void EnterState();
        void Update();
        void LeaveState();
    }
}