using Common.UnitSystem;
using Common.Util.Pickers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common
{
    public class GameSettings : ScriptableObject
    {
        [SerializeField] 
        private ScreenPicker _firstLoadedScreen;

        [SerializeField] 
        private ScreenPicker _loseScreen;

        [SerializeField] 
        private ScreenPicker _winScreen;

        [FormerlySerializedAs("_minDeathForCorona")] [SerializeField] 
        private Stat minDanesForGroup;
        
        [FormerlySerializedAs("_maxTimeForCorona")] [SerializeField] 
        private Stat maxTimeForGroupToDie;

        [SerializeField] 
        private Stat _coronaHealth;

        [SerializeField] 
        private Stat _coronaToLosePerGroupDeath;

        [SerializeField] 
        private Stat _groupDeathTimeout;

        [SerializeField] 
        private UICountDownTimer.Data _gameTimerData;

        public Stat MinDanesForGroup => minDanesForGroup;

        public Stat MaxTimeForGroupToDie => maxTimeForGroupToDie;

        public Stat CoronaHealth => _coronaHealth;

        public Stat CoronaToLosePerGroupDeath => _coronaToLosePerGroupDeath;

        public Stat GroupDeathTimeout => _groupDeathTimeout;

        public string FirstLoadedScreen => _firstLoadedScreen.PickedValue;

        public ScreenPicker WinScreen => _winScreen;

        public ScreenPicker LoseScreen => _loseScreen;

        public UICountDownTimer.Data GameTimerData => _gameTimerData;
    }
}