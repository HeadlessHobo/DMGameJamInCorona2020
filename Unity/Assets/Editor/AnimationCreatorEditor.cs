using System;
using System.Linq;
using Common;
using Common._2DAnimation;
using Common._2DAnimation.SpriteSheet;
using NUnit.Framework.Internal;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(AnimationCreator), true)]
    public class AnimationCreatorEditor : UnityEditor.Editor
    {
        private Texture2D[] _textures;
        private int _index;
        private float _nextIndexUpdateTime;
        private float _nextSpritesUpdateTime;
        private AnimationCreator _target;
        void OnEnable()
        {
            _target = (AnimationCreator) target;
            UpdateSprites();
            EditorApplication.update += Update;
        }

        private void OnDisable()
        {
            EditorApplication.update -= Update;
        }

        private void Update()
        {
            if (_nextIndexUpdateTime <= Time.realtimeSinceStartup)
            {
                _nextIndexUpdateTime = Time.realtimeSinceStartup + 1 / (_target.FramesPerSecond * _target.Speed);
                _index++;
            }

            if (_index >= _textures.Length)
            {
                _index = 0;
            }
            
            EditorUtility.SetDirty(target);
        }
        private void UpdateSprites()
        {
            _textures = _target.GetSpritesAsTextures().ToArray();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Refresh sprites"))
            {
                UpdateSprites();
            }
            GUILayout.Box(_textures[_index]);
        }
    }
}