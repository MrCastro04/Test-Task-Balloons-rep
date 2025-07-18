using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.UI.General_Buttons.Base_Button;
using UnityEngine;

namespace Modules.Core.UI.Screens.Settings_Screen.Change_Volume_Button
{
    public class ChangeVolumeButton : BaseButton
    {
        [SerializeField] private VolumeType _volumeType;
        [SerializeField] private float _delta = 0.1f; 

        protected override void OnClickAction()
        {
            ActionSystem.Instance.Perform(new ChangeVolumeGA(_volumeType, _delta));
        }
    }
}