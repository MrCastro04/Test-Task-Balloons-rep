using UnityEngine;
using UnityEngine.UI;
using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.UI.Screens.Base_Screen;

public class BackButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private BaseScreen _currentScreen;

    private void Awake()
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnBack);
    }

    private void OnBack()
    {
        ActionSystem.Instance.Perform(new CloseScreenGA(_currentScreen));
    }
}