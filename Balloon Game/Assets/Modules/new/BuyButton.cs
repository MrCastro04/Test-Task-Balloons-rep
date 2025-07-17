using Modules.Core.Systems.Action_System.Scripts;
using UnityEngine;

public class BuyButton : BaseButton
{
    [SerializeField] private int balloonIndex;
    [SerializeField] private Sprite balloonSprite;

    public int BalloonIndex => balloonIndex;
    public Sprite BalloonSprite => balloonSprite;

    protected override void OnClickAction()
    {
        ActionSystem.Instance.Perform(new BuyBalloonGA(balloonIndex, balloonSprite));
    }
}