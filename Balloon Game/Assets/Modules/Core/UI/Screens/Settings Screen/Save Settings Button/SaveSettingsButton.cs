using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.UI.General_Buttons.Base_Button;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Core.UI.Screens.Settings_Screen.Save_Settings_Button
{
    public class SaveSettingsButton : BaseButton
    {
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;

        protected override void OnClickAction()
        {
            float sound = _soundSlider.value;
            
            float music = _musicSlider.value;
            
            ActionSystem.Instance.Perform(new SaveSettingsGA(sound, music));
        }
    }
}
