using System.Collections;
using Modules.Core.UI.General_Buttons.Buy_Bolloon_Block;
using Modules.Core.UI.Screens.Base_Screen;
using UnityEngine;

namespace Modules.Core.UI.Screens.Buy_Screen
{
    public class BuyScreen : BaseScreen
    {
        [SerializeField] private BuyBalloonBlock _buyBalloonBlock;

        private int _cashedId;
        private Sprite _cashedSkinOnThisButton;

        public override IEnumerator Open()
        {
            _buyBalloonBlock.SkinId = _cashedId;
            _buyBalloonBlock.SkinSprite = _cashedSkinOnThisButton;
            yield return base.Open();
        }

        public void Load(int id, Sprite balloonBlockSkin)
        {
            _cashedId = id;
            _cashedSkinOnThisButton = balloonBlockSkin;
        }
    }
}