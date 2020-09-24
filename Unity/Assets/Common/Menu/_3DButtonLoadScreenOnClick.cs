using Common.Util.Pickers;
using UnityEngine;

namespace Common.Menu
{
    [RequireComponent(typeof(Collider))]
    public class _3DButtonLoadScreenOnClick : MonoBehaviour
    {
        [SerializeField]
        private ScreenPicker _screenPicker;
        
        private void OnMouseDown() 
        {
            ScreenManager.Instance.LoadScreen(_screenPicker.PickedValue);
        }
    }
}