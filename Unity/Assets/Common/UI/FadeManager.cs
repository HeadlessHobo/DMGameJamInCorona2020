using System;
using Gamelogic.Extensions;
using Plugins.LeanTween.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    public class FadeManager : Singleton<FadeManager>
    {
        private Image _spawnedFaderImage;
        private LTDescr _currentFadeWorker;
        
        [SerializeField]
        private GameObject _faderPrefab;

        public void FadeTo(float alpha, float time)
        {
            if (_currentFadeWorker != null)
            {
                LeanTween.cancel(_currentFadeWorker.id);
            }
            
            if (_spawnedFaderImage == null)
            {
                SpawnFaderPrefab();
            }
            
            _currentFadeWorker = _spawnedFaderImage.rectTransform.LeanAlpha(alpha, time);
        }

        private void SpawnFaderPrefab()
        {
            GameObject spawnedFaderGo = Instantiate(_faderPrefab);
            DontDestroyOnLoad(spawnedFaderGo);

            _spawnedFaderImage = spawnedFaderGo.GetComponentInChildren<Image>();
        }
    }
}