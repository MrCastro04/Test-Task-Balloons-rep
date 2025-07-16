using Modules.Core.Systems.Action_System.Scripts;
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