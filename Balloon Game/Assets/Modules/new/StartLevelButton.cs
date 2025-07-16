using Modules.Core.Systems.Action_System.Scripts;
using UnityEngine;

public class StartLevelSceneButton : BaseButton
{
    [SerializeField] private int _levelIndex;

    protected override void OnClickAction()
    {
        ActionSystem.Instance.Perform(new LoadLevelSceneGA(_levelIndex));
    }
}