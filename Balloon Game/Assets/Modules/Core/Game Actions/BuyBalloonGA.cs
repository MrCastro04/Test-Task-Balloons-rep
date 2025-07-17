using UnityEngine;
using Modules.Core.Systems.Action_System.Scripts;

public class BuyBalloonGA : GameAction
{
    public readonly int BalloonIndex;
    public readonly Sprite BalloonSprite;

    public BuyBalloonGA(int balloonIndex, Sprite balloonSprite)
    {
        BalloonIndex = balloonIndex;
        BalloonSprite = balloonSprite;
    }
}