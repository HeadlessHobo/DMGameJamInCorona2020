using Common.Util.Pickers;
using UnityEngine;

namespace Common
{
    public class GameSettings : ScriptableObject
    {
        [SerializeField] 
        private ScreenPicker _firstLoadedScreen;

        public string FirstLoadedScreen => _firstLoadedScreen.PickedValue;
    }
}