using UnityEngine;

namespace Common.Menu
{
    public class ExitButton : ButtonBase
    {
        protected override void OnClick()
        {
            Application.Quit();
        }
    }
}