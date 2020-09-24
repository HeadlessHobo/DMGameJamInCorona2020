using System.Collections.Generic;
using System.Linq;

namespace Common._2DAnimation.State
{
    public class AnimationStateManager
    {
        private List<IAnimationState> _animationStates;
        private IAnimationState _currentAnimationState;

        public AnimationStateManager(AnimationStateProperties[] animationStateDataEntries, Animator animator)
        {
            AddAnimationStatesFromData(animationStateDataEntries, animator);
            SortAnimationStatesByPriority();
        }

        private void AddAnimationStatesFromData(AnimationStateProperties[] animationStateDataEntries, Animator animator)
        {
            _animationStates = new List<IAnimationState>();
            foreach (var animationStateDataEntry in animationStateDataEntries)
            {
                IAnimationState animationState = animationStateDataEntry.Create(animator);
                _animationStates.Add(animationState);
            }
        }

        private void SortAnimationStatesByPriority()
        {
            _animationStates = _animationStates.OrderBy(item => item.Priority).ToList();
        }

        public void UpdateStates()
        {
            IAnimationState newAnimationStateToTransistionTo = GetAnimationStateToTransitionTo();

            if (_currentAnimationState == null || !_currentAnimationState.CanTransitionTo() || 
                IsNewAnimationStateHigherInPriorityThanCurrent(newAnimationStateToTransistionTo))
            {
                TransitionToNewAnimationState(newAnimationStateToTransistionTo, _currentAnimationState);
            }
              
            _currentAnimationState.Update();
        }

        private bool IsNewAnimationStateHigherInPriorityThanCurrent(IAnimationState newAnimationState)
        {
            return newAnimationState != _currentAnimationState &&
                   newAnimationState.Priority < _currentAnimationState.Priority;
        }

        private void TransitionToNewAnimationState(IAnimationState newAnimationState, IAnimationState oldAnimationState)
        {
            oldAnimationState?.LeaveState();
            newAnimationState.EnterState();
            _currentAnimationState = newAnimationState;
        }

        private IAnimationState GetAnimationStateToTransitionTo()
        {
            return _animationStates.Find(item => item.CanTransitionTo());
        }
        
        public void StopAllAnimationStates()
        {
            foreach (var animationState in _animationStates)
            {
                animationState.LeaveState();
            }

            _currentAnimationState = null;
        }

        public void UpdateDataObj<T>(object dataObj)
        {
            IAnimationState animationStateWithType = _animationStates.Find(item => item.GetType() == typeof(T));
            if (animationStateWithType.SupportsDataObj(dataObj))
            {
                animationStateWithType.UpdateDataObj(dataObj);
            }
        }
    }
}