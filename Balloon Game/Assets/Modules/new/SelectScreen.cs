
    using System.Collections;
    using Modules.Core.UI.Screens.Base_Screen;
    using UnityEngine;

    public class SelectScreen : BaseScreen
    {
        [SerializeField] private SelectBalloonButton _balloonBlockButton;

        private int _cashedId;
        private Sprite _cashedSkinOnThisButton;
        
        public override IEnumerator Open()
        {
           _balloonBlockButton.SkinId = _cashedId;
           _balloonBlockButton.SkinSprite = _cashedSkinOnThisButton;

           yield return null;
        }
        
        public void Load(int id, Sprite balloonBlockSkin)
        {
            _cashedId = id;

            _cashedSkinOnThisButton = balloonBlockSkin;
        }
    }