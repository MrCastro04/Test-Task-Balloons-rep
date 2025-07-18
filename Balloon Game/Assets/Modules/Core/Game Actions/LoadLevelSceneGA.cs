using Modules.Core.Systems.Action_System.Scripts;

namespace Modules.Core.Game_Actions
{
    public class LoadLevelSceneGA : GameAction
    {
        public readonly int LevelIndex;

        public LoadLevelSceneGA(int levelIndex)
        {
            LevelIndex = levelIndex;
        }
    }
}