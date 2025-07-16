using System.Collections;
using UnityEngine;
using TMPro;
using Modules.Core.UI.Screens.Base_Screen;

public class MyProfileScreen : BaseScreen
{
    [SerializeField] private TMP_InputField _nameInputField;
    [SerializeField] private TextMeshProUGUI _playerNameLabel;

    public override IEnumerator Open()
    {
        string savedName = SaveSystem.Instance.LoadPlayerName();
        _playerNameLabel.text = savedName;
        _nameInputField.text = savedName;
        yield return null;
    }

    protected override IEnumerator Exit()
    {
        yield return null;
    }
}