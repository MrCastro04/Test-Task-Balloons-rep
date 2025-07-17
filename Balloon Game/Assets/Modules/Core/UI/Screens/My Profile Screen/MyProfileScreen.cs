using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Modules.Core.UI.Screens.Base_Screen;
using UnityEngine.UI;

public class MyProfileScreen : BaseScreen
{
    [SerializeField] private Image _avatarImage;
    [SerializeField] private TMP_InputField _nameInputField;
    [SerializeField] private TMP_Text _playerNameLabel;
    [SerializeField] private List<Sprite> _availableAvatars;

    public List<Sprite> AvailableAvatars => _availableAvatars;
    public Image AvatarImage => _avatarImage;

    public override IEnumerator Open()
    {
        LoadSavedAvatar();
        string savedName = SaveSystem.Instance.LoadPlayerName();
        _playerNameLabel.text = savedName;
        _nameInputField.text = savedName;

        yield return base.Open();
    }

    private void LoadSavedAvatar()
    {
        string avatarName = SaveSystem.Instance.LoadPlayerAvatarPath();
        if (string.IsNullOrEmpty(avatarName)) return;

        Sprite savedAvatar = _availableAvatars.Find(a => a.name == avatarName);
        
        if (savedAvatar != null)
            _avatarImage.sprite = savedAvatar;
    }

    protected override IEnumerator Exit()
    {
        yield return null;
    }
}