using Modules.Core.Systems.Action_System.Scripts;
using UnityEngine;

public class BuyButton : BaseButton
{
    [SerializeField] private BuyBalloonBlock _balloonBlock;
    
     private int _balloonIndex;
     private Sprite _balloonSprite;

    protected override void OnClickAction()
    {
        _balloonIndex = _balloonBlock.SkinId;

        _balloonSprite = _balloonBlock.SkinSprite;
        
        ActionSystem.Instance.Perform(new BuyBalloonGA(_balloonIndex, _balloonSprite));
    }
}