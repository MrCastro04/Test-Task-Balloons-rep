using Modules.Core.Systems.Action_System.Scripts;

namespace Modules.Core.Game_Actions
{
    public class SavePlayerNameGA : GameAction
    {
        public readonly string PlayerName;

        public SavePlayerNameGA(string playerName)
        {
            PlayerName = playerName;
        }
    }
}