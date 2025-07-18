using Modules.Core.Systems.Action_System.Scripts;

namespace Modules.@new
{
    public class EndLevelGA : GameAction
    {
        public readonly int Score;
        public readonly int Stars;
        public readonly int LevelIndex;

        public EndLevelGA(int score, int stars, int levelIndex)
        {
            Score = score;
            Stars = stars;
            LevelIndex = levelIndex;
        }
    }
}