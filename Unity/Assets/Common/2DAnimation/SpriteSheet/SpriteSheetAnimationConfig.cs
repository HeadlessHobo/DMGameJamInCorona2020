using System;
using Plugins.NaughtyAttributes.Scripts.Core.DrawerAttributes;
using UnityEngine;

namespace Common._2DAnimation.SpriteSheet
{
    [Serializable][CreateAssetMenu(fileName = "Spritesheet config", menuName = "2D Animation/Spritesheet config", order = 54)]
    public class SpriteSheetAnimationConfig : ScriptableObject
    {
        [SerializeField] 
        private Sprite _spriteSheet;

        [SerializeField]
        private Vector2 _gridSize;
        
        [SerializeField]
        private Vector2 _cellSize;
        
        private void Awake()
        {
            CalculateSize();
        }
        
        [Button("CalculateSize")]
        private void CalculateSize()
        {
            if (_cellSize != Vector2.zero && _gridSize == Vector2.zero)
            {
                _gridSize = _spriteSheet.rect.size / _cellSize;
            }
            else if (_gridSize != Vector2.zero && _cellSize == Vector2.zero)
            {
                _cellSize = _spriteSheet.rect.size / _gridSize;
            }
        }
        
        public Sprite[] CreateSpritesFromSpriteSheet(int lengthOfAnimation, Vector2 readDirection, Vector2 startCellPosition)
        {
            Sprite[] sprites = new Sprite[lengthOfAnimation];

            for (int i = 0; i < lengthOfAnimation; i++)
            {
                Vector2 worldCellPosition = CalculateWorldSpriteCellPostion(startCellPosition, readDirection, i);
                sprites[i] = Sprite.Create(_spriteSheet.texture,
                    new Rect(worldCellPosition.x, _spriteSheet.rect.size.y - worldCellPosition.y, _cellSize.x, _cellSize.y),
                    new Vector2(0.5f, 0.5f));
            }

            return sprites;
        }
        
        private Vector2 CalculateWorldSpriteCellPostion(Vector2 startCellPosition, Vector2 readDirection, int index)
        {
            Vector2 spriteCellPosition = startCellPosition + (readDirection * index) + Vector2.up;

            while (spriteCellPosition.x >= _gridSize.x)
            {
                spriteCellPosition.x -= _gridSize.x;
                spriteCellPosition.y++;
            }

            return spriteCellPosition * _cellSize;
        }
    }
}