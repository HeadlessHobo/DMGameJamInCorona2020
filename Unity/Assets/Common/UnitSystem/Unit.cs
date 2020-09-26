using System;
using System.Collections.Generic;
using Common.UnitSystem.LifeCycle;
using Common.UnitSystem.Stats;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common.UnitSystem
{
    [ExecuteInEditMode]
    public abstract class Unit : MonoBehaviour, IUnit
    {
        private LifeCycleHandler _lifeCycleHandler;

        public abstract UnitType UnitType { get; }
        protected abstract IUnitStatsManager StatsManager { get; }
        
        protected abstract IArmor Armor { get; set; }
        
        protected abstract List<object> Setups { get; }

        public T GetArmor<T>() where T : IArmor
        {
            return ConvertObjectAndVerifyType<T>(Armor);
        }
        
        public T GetSetup<T>()
        {
            foreach (var setup in Setups)
            {
                if (setup.GetType() == typeof(T))
                {
                    return ConvertObjectAndVerifyType<T>(setup);
                }
            }

            Debug.LogError(
                $"Tried to get setup with type: { typeof(T).Name } but was unable to find it. ");
            return default(T);
        }

        public T GetStatsManager<T>() where T : IUnitStatsManager
        {
            return ConvertObjectAndVerifyType<T>(StatsManager);
        }
        
        protected T ConvertObjectAndVerifyType<T>(object obj) 
        {
            Type wantedConfigType = typeof(T);
            
            if (VerifyObjectType(obj, typeof(T)))
            {
                return (T) obj;
            }

            Debug.LogError(
                $"Tried to get incorrect obj: {obj.GetType().Name} wantedObjectType: {wantedConfigType} ");
            return default(T);
        }

        protected bool VerifyObjectType(object obj, Type wantedObjectType)
        {
            Type objType = obj.GetType();
            return wantedObjectType.IsInstanceOfType(obj) || objType == wantedObjectType;
        }
        
        protected void AddLifeCycleObject(object obj)
        {
            _lifeCycleHandler.AddLifeCycleObject(obj);
        }
        
        protected void AddLifeCycleObjects(params object[] objs)
        {
            _lifeCycleHandler.AddLifeCycleObjects(objs);
        }

        protected virtual void Awake()
        {
            _lifeCycleHandler = new LifeCycleHandler();
        }

        protected virtual void Update()
        {
            if (!Application.isPlaying)
            {
                EditorUpdate();
            }
            else
            {
                _lifeCycleHandler.Update();
            }
            
        }

        protected virtual void EditorUpdate()
        {
            
        }
        
        protected virtual void FixedUpdate()
        {
            if (Application.isPlaying)
            {
                _lifeCycleHandler?.FixedUpdate();
            }
            
        }
        
        protected virtual void OnDestroy()
        {
            if (Application.isPlaying)
            {
                _lifeCycleHandler?.OnDestroy();
            }
        }

        protected virtual void OnDrawGizmos()
        {
            _lifeCycleHandler?.OnDrawGizmos();
        }
    }
}