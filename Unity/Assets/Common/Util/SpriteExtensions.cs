using UnityEngine;

namespace Common.Util
{
    public static class SpriteExtensions
    {
        public static Texture2D ToTexture(this Sprite sprite)
        {
            if(sprite.rect.width != sprite.texture.width){
                Texture2D newText = new Texture2D((int)sprite.rect.width,(int)sprite.rect.height);
                Color[] newColors = sprite.texture.GetPixels((int)sprite.rect.x, 
                    (int)sprite.rect.y, 
                    (int)sprite.rect.width, 
                    (int)sprite.rect.height );
                newText.SetPixels(newColors);
                newText.Apply();
                return newText;
            } else
                return sprite.texture;
        }
    }
}