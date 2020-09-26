using System;
using UnityEngine;

namespace Common.UnitSystem
{
    [Serializable]
    public class UnitGraphicSetup
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField] 
        private Transform _graphicsTransform;
        
        public Transform GraphicsTransform => _graphicsTransform;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
    }
}