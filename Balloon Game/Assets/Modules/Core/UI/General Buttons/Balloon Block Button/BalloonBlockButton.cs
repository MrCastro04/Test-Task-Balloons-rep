using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.UI.General_Buttons.Base_Button;
using Modules.Core.UI.Screens.Buy_Screen;
using Modules.Core.UI.Screens.Select_Screen;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Core.UI.General_Buttons.Balloon_Block_Button
{
    public class BalloonBlockButton : BaseButton
    {
        [SerializeField] public int SkinId;
        [SerializeField] public Sprite SkinOnThisButton;
        [SerializeField] private BuyScreen _buyScreen;
        [SerializeField] private SelectScreen _selectScreen;
        [SerializeField] private Image _buyTextPhoto;
        [SerializeField] private Image _selectedHighlight;

        public void RemoveBuyTextPhoto() => _buyTextPhoto.gameObject.SetActive(false);

        public void UpdateSelectedHighlight(Sprite selectedSkin)
        {
            if (_selectedHighlight != null)
                _selectedHighlight.gameObject.SetActive(selectedSkin == SkinOnThisButton);
        }

        protected override void OnClickAction()
        {
            ActionSystem.Instance.Perform(new PlayerTapBalloonBlockGA(SkinId, SkinOnThisButton, _buyScreen, _selectScreen));
        }
    }
}