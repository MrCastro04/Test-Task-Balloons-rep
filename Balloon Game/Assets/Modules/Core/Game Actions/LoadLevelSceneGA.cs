using Modules.Core.Systems.Action_System.Scripts;

public class LoadLevelSceneGA : GameAction
{
    public readonly int LevelIndex;

    public LoadLevelSceneGA(int levelIndex)
    {
        LevelIndex = levelIndex;
    }
}