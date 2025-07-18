using UnityEngine;

namespace Modules.@new
{
    [CreateAssetMenu(menuName = "Data/Player")]
    public class PlayerData : ScriptableObject
    {
        [field: SerializeField] public string PlayerName { get; private set; }
        [field: SerializeField] public bool IsPlayer { get; private set; }
        [field: SerializeField] public Sprite AvatarImage { get; private set; }
        [field: SerializeField] public int RewardPoints { get; private set; }
        [field: SerializeField] public int TotalStars { get; private set; }
    }
}