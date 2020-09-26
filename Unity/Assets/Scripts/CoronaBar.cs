using System;
using Common;
using UnityEngine;
using UnityEngine.UI;

public class CoronaBar : MonoBehaviour
{
    [SerializeField]
    private Image _bar;
    
    private void Update()
    {
        _bar.fillAmount = GameManager.Instance.GameSettings.CoronaHealth.CurrentProcent;
    }
}