

using System;
using Common.SpawnHanding;
using Common.Util;
using Gamelogic.Extensions;
using UnityEngine;

namespace Common
{
    public class GameManager : Singleton<GameManager>
    {
        public const string GAME_SETTINGS_SCRIPTABLE_OBJECT_NAME = "GameSettings";
        
        private GameSettings _gameSettings;
        
        public Camera MainCamera => Camera.main;
        public PlayerManager Player { get; private set; }

        public GameSettings GameSettings => _gameSettings;
        private void Awake()
        {
            _gameSettings = ScriptableObjectUtils.Load<GameSettings>(GAME_SETTINGS_SCRIPTABLE_OBJECT_NAME);
            SpawnManager.Instance.Spawn<PlayerManager>(SpawnType.Player, OnPlayerSpawned);
            SpawnManager.Instance.SpawnAllWithType(SpawnType.Dane);
        }

        private void OnPlayerSpawned(PlayerManager player)
        {
            Player = player;
        }
    }
}