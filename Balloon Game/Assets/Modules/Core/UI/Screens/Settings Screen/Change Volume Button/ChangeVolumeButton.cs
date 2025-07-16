using Modules.Core.Systems.Action_System.Scripts;
using UnityEngine;

public class ChangeVolumeButton : BaseButton
{
    [SerializeField] private VolumeType _volumeType;
    [SerializeField] private float _delta = 0.1f; 

    protected override void OnClickAction()
    {
        ActionSystem.Instance.Perform(new ChangeVolumeGA(_volumeType, _delta));
    }
}