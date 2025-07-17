using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private Image _playerAvatar;
    [SerializeField] private TMP_Text _playerRewardPoints;

    private PlayerData _data;

    public bool IsPlayer => _data != null && _data.IsPlayer;
    public string PlayerName => _playerName.text;
    public int RewardPoints => int.Parse(_playerRewardPoints.text);

    public void Init(PlayerData playerData)
    {
        _data = playerData;
        _playerName.text = playerData.PlayerName;
        _playerAvatar.sprite = playerData.AvatarImage;
        _playerRewardPoints.text = playerData.RewardPoints.ToString();
    }

    public void SetPlayer(string name, int rewardPoints, Image avatar)
    {
        _playerName.text = name;

        _playerRewardPoints.text = rewardPoints.ToString();

        _playerAvatar = avatar;
    }
}