using System;
using UnityEngine;

namespace Common.UpgradeSystem
{
    [Serializable]
    public class UpgradeActionSelector
    {
        [SerializeField]
        private int _upgradeActionIndex;

        [SerializeField]
        private string[] _upgradeActionNames;

        public T CreateUpgradeAction<T>() where T : IUpgradeAction
        {
            Type upgradeActionType = Type.GetType(_upgradeActionNames[_upgradeActionIndex]);
            
            object upgradeActionObj = Activator.CreateInstance(upgradeActionType);
                
            return (T)upgradeActionObj;
        }
    }
}