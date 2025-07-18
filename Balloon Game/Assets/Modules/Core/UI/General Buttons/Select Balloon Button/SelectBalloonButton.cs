using Modules.Core.UI.General_Buttons.Base_Button;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Core.UI.General_Buttons.Select_Balloon_Button
{
    public class SelectBalloonButton : BaseButton
    {
        [SerializeField] private int _skinId;
        [SerializeField] private Sprite _skinSprite;
        [SerializeField] private Image _skinImage;
    
        public int SkinId
        {
            get => _skinId;
            set => _skinId = value;
        }

        public Sprite SkinSprite
        {
            get => _skinSprite;
            set
            {
                _skinSprite = value;
                if (_skinImage != null)
                    _skinImage.sprite = _skinSprite;
            }
        }
        protected override void OnClickAction() { }
    }
}