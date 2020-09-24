using System;
using System.Collections.Generic;
using Common._2DAnimation.Abstract;
using Common.Generating;
using Common.Util;
using Plugins.NaughtyAttributes.Scripts.Core.DrawerAttributes;
using UnityEngine;

namespace Common._2DAnimation.SpriteSheet
{
    [Serializable][CreateAssetMenu(fileName = "SpriteSheetAnimationCreator", menuName = "2D Animation/Spritesheet animation", order = 55)]
    public class SpritesheetAnimationCreator : AnimationCreator
    {
        [SerializeField]
        private Vector2 _startCellPosition;
        
        [SerializeField]
        public int _lengthOfAnimation;

        [SerializeField]
        private SpriteSheetReadDirection _readDirection;

        [SerializeField]
        private SpriteSheetAnimationConfig _spriteSheetConfig;

        public override IAnimation CreateAnimation()
        {
            return new SpriteAnimation(_name, _framesPerSecond, _speed,
                _spriteSheetConfig.CreateSpritesFromSpriteSheet(_lengthOfAnimation, GetReadDirectionAsVector(), _startCellPosition));
        }

        protected override Sprite[] GetSpritesOfAnimation()
        {
            return _spriteSheetConfig.CreateSpritesFromSpriteSheet(_lengthOfAnimation, GetReadDirectionAsVector(),
                _startCellPosition);
        }

        private Vector2 GetReadDirectionAsVector()
        {
            switch (_readDirection)
            {
                case SpriteSheetReadDirection.Left:
                    return Vector2.left;
                case SpriteSheetReadDirection.Right:
                    return Vector2.right;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}