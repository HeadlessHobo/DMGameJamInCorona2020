using System.Collections.Generic;
using Common._2DAnimation.Abstract;
using Common.Util;
using UnityEngine;

namespace Common._2DAnimation
{
    public abstract class AnimationCreator : ScriptableObject
    {
        [SerializeField]
        protected string _name;
        
        [SerializeField]
        protected float _framesPerSecond = 10;
        
        [SerializeField]
        protected float _speed = 1;
        
        public string Name => _name;

        public float FramesPerSecond => _framesPerSecond;

        public float Speed => _speed;

        public abstract IAnimation CreateAnimation();

        protected abstract Sprite[] GetSpritesOfAnimation();
        
        public IEnumerable<Texture2D> GetSpritesAsTextures()
        {
            foreach (var sprite in GetSpritesOfAnimation())
            {
                yield return sprite.ToTexture();
            }
        }
    }
}