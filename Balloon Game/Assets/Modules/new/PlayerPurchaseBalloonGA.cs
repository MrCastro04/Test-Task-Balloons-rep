using UnityEngine;
using Modules.Core.Systems.Action_System.Scripts;

public class PlayerPurchaseBalloonGA : GameAction
{
    public readonly int BalloonIndex;
    public readonly Sprite BalloonSprite;

    public PlayerPurchaseBalloonGA(int balloonIndex, Sprite balloonSprite)
    {
        BalloonIndex = balloonIndex;
        BalloonSprite = balloonSprite;
    }
}