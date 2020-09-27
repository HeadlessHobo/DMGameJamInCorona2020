using System;
using Common;
using Common.UnitSystem;
using TMPro;
using UnityEngine;

public class UICountDownTimer : MonoBehaviour
{
    private const int SECONDS_IN_A_MINUTE = 60;
    
    [SerializeField]
    private TextMeshProUGUI _textMeshPro;

    private float _timer;
    private Data _data;
    private bool _timerEnded;


    private void Start()
    {
        _data = GameManager.Instance.GameSettings.GameTimerData;
        _timer = _data.GetTimeAsSeconds();
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0 && !_timerEnded)
        {
            _textMeshPro.text = "0M 0S";
            _timerEnded = true;
            GameManager.Instance.OnGameEnded();
        }
        else if(_timer > 0 && !_timerEnded)
        {
            _textMeshPro.text = $"{GetMinutesLeft()}M {GetSeconds():F0}S";
        }

        
    }

    private int GetMinutesLeft()
    {
        return Mathf.FloorToInt(_timer / SECONDS_IN_A_MINUTE);
    }

    private float GetSeconds()
    {
        return _timer - (GetMinutesLeft() * SECONDS_IN_A_MINUTE);
    }
    
    [Serializable]
    public class Data
    {
        public Stat Minutes;
        public Stat Seconds;

        public float GetTimeAsSeconds()
        {
            return Minutes.Value * SECONDS_IN_A_MINUTE + Seconds.Value;
        }
    }
}