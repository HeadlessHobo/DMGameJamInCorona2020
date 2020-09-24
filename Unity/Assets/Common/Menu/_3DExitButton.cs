using UnityEngine;

namespace Common.Menu
{
    [RequireComponent(typeof(Collider))]
    public class _3DExitButton : MonoBehaviour
    {
        private void OnMouseDown() 
        {
            Application.Quit();
        }
    }
}