using System;
using System.Collections.Generic;
using Common.Menu.ScreenLoaders;
using Common.Menu.ScreenLoaders._3D;
using Common.Menu.ScreenLoaders.Scene;
using Common.Menu.TransitionEffects;
using Common.Util;
using Common.Util.Pickers;
using Gamelogic.Extensions;
using UnityEngine;

namespace Common.Menu
{
    public class ScreenManager : Singleton<ScreenManager>
    {
        private const bool IS_RELEASE_MODE_ACTIVE = false;
        
        [SerializeField]
        private ScreenLoadersData _screenLoadersData;

        [SerializeField] 
        private TransitionEffectManager _transitionEffectManager;
        
        private List<IScreenLoader> _screenLoaders;
        private IScreenLoader _currrentlyLoadedScreen;

        public event Action<string> ScreenLoaded;

        private void Awake()
        {
            CreateScreenLoaders();
        }

        private void Start()
        {
            if (IS_RELEASE_MODE_ACTIVE || !Application.isEditor)
            {
                LoadScreen(GameManager.Instance.GameSettings.FirstLoadedScreen);
            }
        }

        private void CreateScreenLoaders()
        {
            _screenLoaders = new List<IScreenLoader>();
            foreach (var sceneLoaderData in _screenLoadersData.SceneLoaderDataEntries)
            {
                _screenLoaders.Add(new SceneLoader(sceneLoaderData));
            }
            
            foreach (var _3dScreenLoaderData in _screenLoadersData._3DScreenLoaderDataEntries)
            {
                
                _screenLoaders.Add(new _3DScreenLoader(_3dScreenLoaderData, () => Camera.main));
            }
        }


        public void LoadScreen(string screenName)
        {
            IScreenLoader screenLoader = _screenLoaders.Find(item => item.ScreenName == screenName);
            if (_currrentlyLoadedScreen != null)
            {
                UnloadCurrentScreenAndLoadNewScreen(screenLoader);
            }
            else
            {
                LoadNewScreen(screenLoader);
            }
        }

        private void UnloadCurrentScreenAndLoadNewScreen(IScreenLoader newScreenLoader)
        {
            _transitionEffectManager.ApplyTransitionEffect(_currrentlyLoadedScreen.OutTransitionEffectName, TransitionType.Out,
                () => PreviousScreenIsGone(newScreenLoader));
        }
        
        private void PreviousScreenIsGone(IScreenLoader newScreenLoader)
        {
            LoadNewScreen(newScreenLoader);
        }

        private void LoadNewScreen(IScreenLoader newScreenLoader)
        {
            newScreenLoader.ScreenLoaded += OnScreenLoaded;
            newScreenLoader.LoadScreen();
            _transitionEffectManager.ApplyTransitionEffect(newScreenLoader.InTransitionEffectName, TransitionType.In);
            _currrentlyLoadedScreen = newScreenLoader;
            
        }

        private void OnScreenLoaded(IScreenLoader screenLoaded)
        {
            screenLoaded.ScreenLoaded -= OnScreenLoaded;
            ScreenLoaded?.Invoke(screenLoaded.ScreenName);
        }

        public List<string> GetAllScreenNames()
        {
            List<string> screenNames = new List<string>();

            if (!Application.isPlaying && Application.isEditor)
            {
                CreateScreenLoaders();
            }

            foreach (var screenLoader in _screenLoaders)
            {
                screenNames.Add(screenLoader.ScreenName);
            }

            return screenNames;
        }

        public void LoadScreenWithoutTransitionsFromEditor(string screenName)
        {
            CreateScreenLoaders();
            _screenLoaders.Find(item => item.ScreenName == screenName).LoadScreen();
        }
    }
}