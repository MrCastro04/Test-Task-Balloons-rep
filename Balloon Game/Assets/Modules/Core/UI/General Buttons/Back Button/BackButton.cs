using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.UI.Screens.Base_Screen;
using UnityEngine;

namespace Modules.Core.UI.Back_Button
{
    public class BackButton : BaseButton
    {
        [SerializeField] private BaseScreen _currentScreen;

        protected override void OnClickAction()
        {
            if (_currentScreen != null)
                ActionSystem.Instance.Perform(new CloseScreenGA(_currentScreen));
        }
    }
}