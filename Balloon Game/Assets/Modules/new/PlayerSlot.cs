using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private Image _playerAvatar;
    [SerializeField] private TMP_Text _playerRewardPoints;
    
    [Header("Stars Info")]
    [SerializeField] private Image _starImage;
    [SerializeField] private TMP_Text _playerStarsText;

    private PlayerData _data;

    public bool IsPlayer => _data != null && _data.IsPlayer;
    public string PlayerName => _playerName.text;
    public int RewardPoints => int.Parse(_playerRewardPoints.text);
    public int TotalStars => int.Parse(_playerStarsText.text);

    public void Init(PlayerData playerData, Sprite starSprite)
    {
        _data = playerData;
        _playerName.text = playerData.PlayerName;
        _playerAvatar.sprite = playerData.AvatarImage;
        _playerRewardPoints.text = playerData.RewardPoints.ToString();
        _playerStarsText.text = playerData.TotalStars.ToString();
        
        if (_starImage != null)
        {
            _starImage.sprite = starSprite;
        }
    }

    public void SetPlayer(string name, int rewardPoints, Image avatar, int totalStars)
    {
        _playerName.text = name;
        _playerRewardPoints.text = rewardPoints.ToString();
        _playerStarsText.text = totalStars.ToString();
        
        if (avatar != null)
        {
            _playerAvatar.sprite = avatar.sprite;
        }
    }
}