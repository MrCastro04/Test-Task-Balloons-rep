using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.UI.Screens.Base_Screen;
using Modules.New;

namespace Modules.Core.Game_Actions
{
    public class OpenScreenGA : GameAction
    {
        public readonly BaseScreen NextScreen;

        public OpenScreenGA(BaseScreen nextScreen)
        {
            NextScreen = nextScreen;
        }
    }
}