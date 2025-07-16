using Modules.Core.Systems.Action_System.Scripts;
using UnityEngine;

namespace Modules.Core.UI.Screens.Level_Screen.Load_Scene_Button
{
    public class LoadSceneButton : BaseButton
    {
        [SerializeField] private int _levelIndex;

        protected override void OnClickAction()
        {
            ActionSystem.Instance.Perform(new LoadLevelSceneGA(_levelIndex));
        }
    }
}