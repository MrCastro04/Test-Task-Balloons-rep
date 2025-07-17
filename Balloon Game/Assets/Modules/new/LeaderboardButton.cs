using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using UnityEngine;

public class LeaderboardButton : BaseButton
{
    [SerializeField] private LeaderBoardScreen _leaderBoardScreen;

    protected override void OnClickAction()
    {
        ActionSystem.Instance.Perform(new OpenScreenGA(_leaderBoardScreen));
    }
}