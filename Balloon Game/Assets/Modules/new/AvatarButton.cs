using UnityEngine;

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