using System;
using UnityEngine;

namespace Common.UnitSystem
{
    [Serializable]
    public class UnitSetup
    {
        [SerializeField]
        private GameObject _rootGo;
        public GameObject RootGo => _rootGo;
    }
}