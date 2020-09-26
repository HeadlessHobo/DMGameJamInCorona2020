using System;
using System.Collections.Generic;
using Common.UnitSystem;
using Common.UnitSystem.LifeCycle;
using Common.Util;
using Enemies;
using UnityEngine;

namespace Player
{
    public class PlayerAttractedManager
    {
        private Data _data;
        private GameObject _attractedTriggerGo;
        private GameObject _cheerTriggerGo;
        
        public PlayerAttractedManager(Data data, GameObject attractedTriggerGo, GameObject cheerTriggerGo)
        {
            _data = data;

            SetupTrigger(attractedTriggerGo, data.AttractedTriggerRadius.Value);
            SetupTrigger(cheerTriggerGo, data.CheerTriggerRadius.Value);
            
            SetupTriggerNotifier(attractedTriggerGo, new List<UnitType>() {UnitType.Enemy}, OnEnteredAttractedTrigger,
                OnExitedAttractedTrigger);
        
            SetupTriggerNotifier(cheerTriggerGo, new List<UnitType>() {UnitType.Enemy}, OnEnteredCheerTrigger,
                OnExitedCheerTrigger);
        }
        
        public static void UpdateTriggers(Data data, GameObject attractedTriggerGo, GameObject cheerTriggerGo)
        {
            SetupTrigger(attractedTriggerGo, data.AttractedTriggerRadius.Value);
            SetupTrigger(cheerTriggerGo, data.CheerTriggerRadius.Value);
        }

        private static void SetupTrigger(GameObject gameObject, float radius)
        {
            gameObject.GetComponentInChildren<CircleCollider2D>().radius = radius;
        }

        private void SetupTriggerNotifier(GameObject gameObject, List<UnitType> unitTypes, Action<UnitType, IUnit> onUnitEntered, Action<UnitType, IUnit> onUnitExited)
        {
            TriggerNotifier triggerNotifier = gameObject.AddComponent<TriggerNotifier>();
            triggerNotifier.Init(unitTypes);
            triggerNotifier.UnitEntered += (type, unit) =>  onUnitEntered?.Invoke(type, unit);
            triggerNotifier.UnitExited += (type, unit) =>  onUnitExited?.Invoke(type, unit);
        }

        private void OnEnteredAttractedTrigger(UnitType unitType, IUnit unit)
        {
            (unit as Dane)?.SetNewState(DaneState.Attracted);
        }
    
        private void OnExitedAttractedTrigger(UnitType unitType, IUnit unit)
        {
            (unit as Dane)?.SetNewState(DaneState.Standing);
        }
    
        private void OnEnteredCheerTrigger(UnitType unitType, IUnit unit)
        {
            (unit as Dane)?.SetNewState(DaneState.Cheer);
        }
    
        private void OnExitedCheerTrigger(UnitType unitType, IUnit unit)
        {
            (unit as Dane)?.SetNewState(DaneState.Attracted);
        }

        [Serializable]
        public class Data
        {
            public Stat AttractedTriggerRadius;
            public Stat CheerTriggerRadius;
        }
    }
}