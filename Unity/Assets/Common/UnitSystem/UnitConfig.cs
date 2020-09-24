using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.UnitSystem
{
    [Serializable]
    public abstract class UnitConfig
    { 
        [SerializeField]
        private UnitType _type;
        
        [SerializeField] 
        private List<UnitType> _targetTypes;

        public UnitType Type => _type;

        public List<UnitType> TargetTypes => _targetTypes;
    }
}