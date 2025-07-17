using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Modules.Core.UI.Screens.Base_Screen;
using TMPro;
using UnityEngine.UI;

public class LeaderBoardScreen : BaseScreen
{
    [SerializeField] private PlayerData[] _playersData;
    [SerializeField] private PlayerSlot[] _playerSlots;

    [Header("Player Info Block")]
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private Image _playerAvatar;
    [SerializeField] private TMP_Text _playerRewardPoints;
    [SerializeField] private MyProfileScreen _myProfileScreen;
    
    public override IEnumerator Open()
    {
        FillSlots();
        UpdatePlayerSlot();
        SortByRewardPoints();
        yield return null;
    }

    private void FillSlots()
    {
        for (int i = 0; i < _playerSlots.Length && i < _playersData.Length; i++)
        {
            _playerSlots[i].Init(_playersData[i]);
        }
    }

    private void UpdatePlayerSlot()
    {
        foreach (var slot in _playerSlots)
        {
            if (!slot.IsPlayer) continue;

            string playerName = SaveSystem.Instance.LoadPlayerName();
            int rewardPoints = SaveSystem.Instance.LoadLastReward();

            _playerName.text = playerName;
            _playerRewardPoints.text = rewardPoints.ToString();
            
            _myProfileScreen.LoadSavedAvatar();

            _playerAvatar = _myProfileScreen.AvatarImage;
            
            slot.SetPlayer(playerName,rewardPoints, _playerAvatar);
            
            break;
        }
    }

    private void SortByRewardPoints()
    {
        List<PlayerSlot> sortedSlots = _playerSlots
            .OrderByDescending(slot => slot.RewardPoints)
            .ToList();

        for (int i = 0; i < sortedSlots.Count; i++)
        {
            sortedSlots[i].transform.SetSiblingIndex(i);
        }
    }
}
