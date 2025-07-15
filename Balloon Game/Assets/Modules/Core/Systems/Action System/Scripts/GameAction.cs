using System.Collections.Generic;

namespace Modules.Core.Systems.Action_System.Scripts
{
    public abstract class GameAction
    {
        public readonly List<GameAction> PreReactions = new();
        public readonly List<GameAction> PerformReactions = new();
        public readonly List<GameAction> PostReactions = new();
    }
}