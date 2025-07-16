using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.UI.Screens.Base_Screen;
using UnityEngine;

namespace Modules.Core.UI.Open_Next_Screen_Button
{
    public class OpenNextScreenButton : BaseButton
    {
        [SerializeField] private BaseScreen _nextTargetScreen;

        protected override void OnClickAction()
        {
            if (_nextTargetScreen != null)
                ActionSystem.Instance.Perform(new OpenScreenGA(_nextTargetScreen));
        }
    }
}