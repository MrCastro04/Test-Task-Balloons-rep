using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.UI.Screens.Buy_Screen;
using Modules.Core.UI.Screens.Select_Screen;
using UnityEngine;

namespace Modules.Core.Game_Actions
{
    public class PlayerTapBalloonBlockGA : GameAction
    {
        public readonly int ID;
        public readonly Sprite BalloonBlockSkin;
        public readonly BuyScreen BuyScreen;
        public readonly SelectScreen SelectScreen;

        public PlayerTapBalloonBlockGA(int id, Sprite balloonBlockSkin, BuyScreen buyScreen, SelectScreen selectScreen)
        {
            ID = id;
            BalloonBlockSkin = balloonBlockSkin;
            BuyScreen = buyScreen;
            SelectScreen = selectScreen;
        }
    }
}