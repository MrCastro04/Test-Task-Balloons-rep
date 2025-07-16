using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Modules.Core.Systems.Action_System.Scripts;

public class SavePlayerNameButton : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameInputField;
    [SerializeField] private Button _saveButton;

    private void Awake()
    {
        _saveButton.onClick.AddListener(SaveName);
    }

    private void SaveName()
    {
        string playerName = _nameInputField.text;
        ActionSystem.Instance.Perform(new SavePlayerNameGA(playerName));
    }
}