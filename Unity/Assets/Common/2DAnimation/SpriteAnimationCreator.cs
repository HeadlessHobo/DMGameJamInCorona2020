using System;
using Common._2DAnimation.Abstract;
using UnityEngine;

namespace Common._2DAnimation
{
    [Serializable][CreateAssetMenu(fileName = "SpriteAnimation", menuName = "2D Animation/Sprite animation", order = 56)]
    public class SpriteAnimationCreator : AnimationCreator
    {
        [SerializeField]
        private Sprite[] _sprites;
        
        public override IAnimation CreateAnimation()
        {
            return new SpriteAnimation(_name, _framesPerSecond, _speed, _sprites);
        }

        protected override Sprite[] GetSpritesOfAnimation()
        {
            return _sprites;
        }
    }
}