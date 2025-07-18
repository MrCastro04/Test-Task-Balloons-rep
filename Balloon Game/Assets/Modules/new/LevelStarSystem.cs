using Modules.Core.Utility.Singleton;

namespace Modules.@new
{
    public class LevelStarSystem : Singleton<LevelStarSystem>
    {
        public int CalculateStars(int currentScore, int targetScore)
        {
            if (currentScore == targetScore) return 3;
            if (currentScore >= targetScore * 0.66f) return 2;
            if (currentScore >= targetScore * 0.33f) return 1;
            return 0;
        }
    }
}