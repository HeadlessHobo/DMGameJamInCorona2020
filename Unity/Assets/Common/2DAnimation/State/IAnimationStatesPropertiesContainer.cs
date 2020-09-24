using System.Collections.Generic;

namespace Common._2DAnimation.State
{
    public interface IAnimationStatesPropertiesContainer
    {
        List<AnimationStateProperties> GetAllAnimationStateDataEntires();
    }
}