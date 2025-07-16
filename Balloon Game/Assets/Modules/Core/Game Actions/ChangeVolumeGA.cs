using Modules.Core.Systems.Action_System.Scripts;

public enum VolumeType
{
    Music,
    Sound
}

public class ChangeVolumeGA : GameAction
{
    public readonly VolumeType VolumeType;
    public readonly float Delta;

    public ChangeVolumeGA(VolumeType volumeType, float delta)
    {
        VolumeType = volumeType;
        Delta = delta;
    }
}