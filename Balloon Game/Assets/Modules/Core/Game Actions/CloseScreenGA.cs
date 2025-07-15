using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.UI.Screens.Base_Screen;
using Modules.New;

namespace Modules.Core.Game_Actions
{
    public class CloseScreenGA : GameAction 
    {
        public readonly BaseScreen CurrentScreen;

        public CloseScreenGA(BaseScreen currentScreen)
        {
            CurrentScreen = currentScreen;
        }
    }
}