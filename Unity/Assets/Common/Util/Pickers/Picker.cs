using UnityEngine;

namespace Common.Util.Pickers
{
    public abstract class Picker
    {
        [SerializeField]
        private string _pickedValue;

        public string PickedValue => _pickedValue;
    }
}