using System;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using Yurowm.DebugTools;

namespace Common.Menu
{
    public abstract class ButtonBase : MonoBehaviour
    {
        protected Button _button;
        
        protected virtual void Awake()
        {
            _button = GetComponentInParent<Button>();
            _button.onClick.AddListener(OnClick);
        }
        protected abstract void OnClick();
    }
}