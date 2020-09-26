using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.UnitSystem.LifeCycle
{
    public class LifeCycleHandler : IUpdate, IFixedUpdate, IOnDestroy, IOnDrawGizmos
    {
        private List<IUpdate> _updateObjects;
        private List<IFixedUpdate> _fixedUpdateObjects;
        private List<IOnDestroy> _onDestroyObjects;
        private List<IOnDrawGizmos> _gizmosObjects;

        public LifeCycleHandler()
        {
            _updateObjects = new List<IUpdate>();
            _fixedUpdateObjects = new List<IFixedUpdate>();
            _onDestroyObjects = new List<IOnDestroy>();
            _gizmosObjects = new List<IOnDrawGizmos>();
        }
        
        public void AddLifeCycleObjects(object[] objs)
        {
            foreach (var obj in objs)
            {
                AddLifeCycleObject(obj);
            }
        }

        public void AddLifeCycleObject(object obj)
        {
            AddToListIfObjInheritsFromType(obj, typeof(IUpdate), _updateObjects);
            AddToListIfObjInheritsFromType(obj, typeof(IFixedUpdate), _fixedUpdateObjects);
            AddToListIfObjInheritsFromType(obj, typeof(IOnDestroy), _onDestroyObjects);
            AddToListIfObjInheritsFromType(obj, typeof(IOnDrawGizmos), _gizmosObjects);
        }

        private void AddToListIfObjInheritsFromType(object obj, Type type, IList list)
        {
            Type objType = obj.GetType();

            if (type.IsAssignableFrom(objType))
            {
                list.Add(obj);
            }
        }


        public void Update()
        {
            foreach (var updateObject in _updateObjects)
            {
                updateObject.Update();
            }
        }

        public void FixedUpdate()
        {
            foreach (var fixedUpdateObject in _fixedUpdateObjects)
            {
                fixedUpdateObject.FixedUpdate();
            }
        }
        
        public void OnDrawGizmos()
        {
            foreach (var gizmosObject in _gizmosObjects)
            {
                gizmosObject.OnDrawGizmos();
            }
        }

        public void OnDestroy()
        {
            foreach (var onDestroyObject in _onDestroyObjects)
            {
                onDestroyObject.OnDestroy();
            }
        }
    }
}