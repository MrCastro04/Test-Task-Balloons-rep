using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.UI.General_Buttons.Base_Button;
using TMPro;
using UnityEngine;

namespace Modules.Core.UI.Screens.My_Profile_Screen.Save_Player_Name_Button
{
    public class SavePlayerNameButton : BaseButton
    {
        [SerializeField] private TMP_InputField _nameInputField;

        protected override void OnClickAction()
        {
            string playerName = _nameInputField.text;
            
            ActionSystem.Instance.Perform(new SavePlayerNameGA(playerName));
        }
    }
}