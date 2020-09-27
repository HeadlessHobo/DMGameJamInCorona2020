

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Menu;
using Common.SpawnHanding;
using Common.UnitSystem;
using Common.Util;
using Enemies;
using Gamelogic.Extensions;
using Plugins.Timer.Source;
using UnityEngine;
using Yurowm.DebugTools;

namespace Common
{
    public class GameManager : Singleton<GameManager>
    {
        public const string GAME_SETTINGS_SCRIPTABLE_OBJECT_NAME = "GameSettings";

        private GameSettings _gameSettings;
        private Dictionary<float, Dane> _latestCoronaDaneDeaths;
        private bool _isOnGroupDeathTimeout;
        private bool _aGroupOfDanesHasDied;
        private Timer _groupOfDanesDiedCheckTimer;
        
        public Camera MainCamera => Camera.main;
        public PlayerManager Player { get; private set; }
        public List<Dane> Danes { get; private set; }

        public int DaneCount => Danes.Count;
        public event Action<Dane> DaneDied;
        public event Action GroupOfDanesDied;

        public GameSettings GameSettings => _gameSettings;
        private void Awake()
        {
            Danes = new List<Dane>();
            _latestCoronaDaneDeaths = new Dictionary<float, Dane>();
            _gameSettings = ScriptableObjectUtils.Load<GameSettings>(GAME_SETTINGS_SCRIPTABLE_OBJECT_NAME);
            _gameSettings = Instantiate(_gameSettings);

            HandleSpawning();
            ScreenManager.Instance.ScreenLoaded += OnScreenLoaded;
        }

        private void OnScreenLoaded(string screenName)
        {
            HandleSpawning();
        }

        private void HandleSpawning()
        {
            if (SpawnManager.Instance != null)
            {
                SpawnManager.Instance.Spawn<PlayerManager>(SpawnType.Player, OnPlayerSpawned);
            }
        }


        private void OnPlayerSpawned(PlayerManager player)
        {
            Player = player;
        }

        public void AddDane(Dane dane)
        {
            dane.GetArmor<IArmor>().Died += (killedBy) => OnDaneDied(dane);
        }

        private void OnDaneDied(Dane daneDied)
        {
            DaneDied?.Invoke(daneDied);
            CheckIfAGroupOfDanesHasDied(daneDied);
            DebugPanel.Log("DaneCount", "GameManager", DaneCount);
        }
        
        private void CheckIfAGroupOfDanesHasDied(Dane newDaneDied)
        {
            _latestCoronaDaneDeaths.Add(Time.realtimeSinceStartup, newDaneDied);
            UpdateLatestDaneDeaths();
            if (_latestCoronaDaneDeaths.Count >= _gameSettings.MinDanesForGroup.Value && !_isOnGroupDeathTimeout)
            {
                SoundManager.PlaySFX("CoronaDead");
                _latestCoronaDaneDeaths.Clear();
                _gameSettings.CoronaHealth.DecreaseStat(_gameSettings.CoronaToLosePerGroupDeath.Value);
                GroupOfDanesDied?.Invoke();
                _isOnGroupDeathTimeout = true;
                _aGroupOfDanesHasDied = true;
                Timer.Cancel(_groupOfDanesDiedCheckTimer);
                Timer.Register(_gameSettings.GroupDeathTimeout.Value, () => _isOnGroupDeathTimeout = false);
                Debug.Log("Killed enough for Corona to activate");
            }
            else
            {
                _aGroupOfDanesHasDied = false;
                _groupOfDanesDiedCheckTimer = Timer.Register(_gameSettings.MaxTimeForGroupToDie.Value, () =>
                {
                    if (!_aGroupOfDanesHasDied)
                    {
                        SoundManager.PlaySFX("Wrong");
                    }
                });
            }
        }

        private void UpdateLatestDaneDeaths()
        {
            for (int i = 0; i < _latestCoronaDaneDeaths.Count; i++)
            {
                KeyValuePair<float, Dane> keyValuePair = _latestCoronaDaneDeaths.ElementAt(i);
                if (Time.realtimeSinceStartup - keyValuePair.Key > _gameSettings.MaxTimeForGroupToDie.Value)
                {
                    _latestCoronaDaneDeaths.Remove(keyValuePair.Key);
                }
            }
        }

        public void OnGameEnded()
        {
            if (_gameSettings.CoronaHealth.Value > 0)
            {
                ScreenManager.Instance.LoadScreen(_gameSettings.LoseScreen.PickedValue);
            }
            else
            {
                OnWon();
            }
        }

        public void OnWon()
        {
            ScreenManager.Instance.LoadScreen(_gameSettings.WinScreen.PickedValue);
        }
        
    }
}