using System;
using Common.UnitSystem.LifeCycle;
using Plugins.LeanTween.Framework;
using UnityEngine;

namespace Common.Util
{
    public class Bar : IUpdate
    {
        private Transform _barTransform;
        private Func<float> _getValueFunc;
        private int _minValue;
        private int _maxValue;
        
        public Bar(Transform transform, Func<float> getValueFunc, int minValue = 0, int maxValue = 1)
        {
            _barTransform = transform;
            _getValueFunc = getValueFunc;
            _minValue = minValue;
            _maxValue = maxValue;
        }
        
        public void Update()
        {
            _barTransform.LeanScaleX(GetCurrentValue(), 0.1f);
        }

        private float GetCurrentValue()
        {
            return Mathf.Clamp(_getValueFunc(), _minValue, _maxValue);
        }
    }
}