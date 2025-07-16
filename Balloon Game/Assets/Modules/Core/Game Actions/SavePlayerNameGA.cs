using Modules.Core.Systems.Action_System.Scripts;

public class SavePlayerNameGA : GameAction
{
    public readonly string PlayerName;

    public SavePlayerNameGA(string playerName)
    {
        PlayerName = playerName;
    }
}