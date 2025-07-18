using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.Systems.Balloon_Skin_System;
using Modules.Core.UI.General_Buttons.Base_Button;
using Modules.Core.UI.Screens.Select_Screen;
using UnityEngine;

namespace Modules.Core.UI.General_Buttons.Select_Button
{
    public class SelectButton : BaseButton
    {
        [SerializeField] private BalloonSkinSystem _balloonSkinSystem;
        [SerializeField] private SelectScreen _selectScreen;
        private Sprite _balloonSprite;

        protected override void OnClickAction()
        {
            _balloonSkinSystem.SetSelectedSkin(_balloonSprite); 
        
            ActionSystem.Instance.Perform(new CloseScreenGA(_selectScreen));
        }
    }
}