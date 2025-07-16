using UnityEngine;
using UnityEngine.UI;
using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.UI.Screens.Base_Screen;

namespace Modules.New
{
    public class ButtonMyProfile : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private BaseScreen _myProfileScreen;

        private void Awake()
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(OpenMyProfile);
        }

        private void OpenMyProfile()
        {
            ActionSystem.Instance.Perform(new OpenScreenGA(_myProfileScreen));
        }
    }
}