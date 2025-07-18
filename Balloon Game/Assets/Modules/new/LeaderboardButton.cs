using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.UI.General_Buttons.Base_Button;
using UnityEngine;

namespace Modules.@new
{
    public class LeaderboardButton : BaseButton
    {
        [SerializeField] private LeaderBoardScreen _leaderBoardScreen;

        protected override void OnClickAction()
        {
            ActionSystem.Instance.Perform(new OpenScreenGA(_leaderBoardScreen));
        }
    }
}