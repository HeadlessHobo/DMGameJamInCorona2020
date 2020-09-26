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

        [FormerlySerializedAs("_minDeathForCorona")] [SerializeField] 
        private Stat minDanesForGroup;
        
        [FormerlySerializedAs("_maxTimeForCorona")] [SerializeField] 
        private Stat maxTimeForGroupToDie;

        [SerializeField] 
        private Stat _coronaHealth;

        [SerializeField] 
        private Stat _coronaToLosePerGroupDeath;

        public Stat MinDanesForGroup => minDanesForGroup;

        public Stat MaxTimeForGroupToDie => maxTimeForGroupToDie;

        public Stat CoronaHealth => _coronaHealth;

        public Stat CoronaToLosePerGroupDeath => _coronaToLosePerGroupDeath;    

        public string FirstLoadedScreen => _firstLoadedScreen.PickedValue;
    }
}