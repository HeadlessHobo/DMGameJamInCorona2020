using System;
using Common;
using UnityEngine;
using UnityEngine.UI;

public class CoronaBar : MonoBehaviour
{
    [SerializeField]
    private Image _bar;

    [SerializeField] 
    private UI_Animations _uiAnimations;

    private void Start()
    {
        GameManager.Instance.GroupOfDanesDied += OnGroupOfDanesDied;
    }

    private void OnGroupOfDanesDied()
    {
        _uiAnimations.CoronaHitAni();
    }

    private void Update()
    {
        if (GameManager.Instance.GameSettings.CoronaHealth.Value <= 0)
        {
            GameManager.Instance.OnWon();
        }
        _bar.fillAmount = GameManager.Instance.GameSettings.CoronaHealth.CurrentProcent;
    }
}