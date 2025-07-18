using Modules.Core.UI.General_Buttons.Base_Button;
using UnityEngine;

namespace Modules.@new
{
    public class ExitButton : BaseButton
    {
        protected override void OnClickAction()
        {
            Application.Quit();
        }
    }
}