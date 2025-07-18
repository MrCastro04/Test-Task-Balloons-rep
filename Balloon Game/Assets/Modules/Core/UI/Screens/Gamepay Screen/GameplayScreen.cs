using System.Collections;
using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.UI.Screens.Base_Screen;

namespace Modules.Core.UI.Screens.Gamepay_Screen
{
    public class GameplayScreen : BaseScreen
    {
        public override IEnumerator Open()
        {
            ActionSystem.Instance.AddReaction(new StartLevelGA());
        
            yield return null;
        }
    }
}