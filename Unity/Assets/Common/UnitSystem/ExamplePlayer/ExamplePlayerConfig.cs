using System;
using UnityEngine;

namespace Common.UnitSystem.ExamplePlayer
{
    [Serializable]
    public class ExamplePlayerConfig : UnitConfig
    {
        [SerializeField] 
        private string _name;
        public string Name => _name;
    }
}