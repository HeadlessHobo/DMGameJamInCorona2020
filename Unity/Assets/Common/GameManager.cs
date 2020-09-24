

using System;
using Common.Util;
using Gamelogic.Extensions;
using UnityEngine;

namespace Common
{
    public class GameManager : Singleton<GameManager>
    {
        public const string GAME_SETTINGS_SCRIPTABLE__OBJECT_NAME = "GameSettings";
        
        private GameSettings _gameSettings;
        
        public Camera MainCamera => Camera.main;

        public GameSettings GameSettings => _gameSettings;
        private void Awake()
        {
            _gameSettings = ScriptableObjectUtils.Load<GameSettings>(GAME_SETTINGS_SCRIPTABLE__OBJECT_NAME);
        }
    }
}