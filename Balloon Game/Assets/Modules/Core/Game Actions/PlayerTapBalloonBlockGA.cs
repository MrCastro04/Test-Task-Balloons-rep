using Modules.Core.Systems.Action_System.Scripts;
using UnityEngine;

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