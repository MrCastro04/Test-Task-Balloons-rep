using Modules.Core.Systems.Save_System;
using Modules.Core.UI.General_Buttons.Base_Button;
using UnityEngine;

namespace Modules.Core.UI.Screens.My_Profile_Screen.Avatar_Button
{
    public class AvatarButton : BaseButton
    {
        [SerializeField] private MyProfileScreen _myProfileScreen;

        protected override void OnClickAction()
        {
            if (_myProfileScreen.AvailableAvatars.Count == 0) return;

            Sprite randomAvatar = _myProfileScreen.AvailableAvatars[Random.Range(0, _myProfileScreen.AvailableAvatars.Count)];
            _myProfileScreen.AvatarImage.sprite = randomAvatar;
        
            if (_myProfileScreen.AvatarImage != null)
                _myProfileScreen.AvatarImage.raycastTarget = true;

            SaveSystem.Instance.SavePlayerAvatarPath(randomAvatar.name);
        }

    }
}