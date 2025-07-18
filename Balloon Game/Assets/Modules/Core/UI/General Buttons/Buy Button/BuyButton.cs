using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.UI.General_Buttons.Base_Button;
using Modules.Core.UI.General_Buttons.Buy_Bolloon_Block;
using UnityEngine;

namespace Modules.Core.UI.General_Buttons.Buy_Button
{
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
}