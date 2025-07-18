using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Modules.Core.Systems.Save_System;
using Modules.Core.UI.Screens.Base_Screen;
using Modules.Core.UI.Screens.My_Profile_Screen;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.@new
{
    public class LeaderBoardScreen : BaseScreen
    {
        [SerializeField] private PlayerData[] _playersData;
        [SerializeField] private PlayerSlot[] _playerSlots;

        [Header("Player Info Block")]
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private Image _playerAvatar;
        [SerializeField] private TMP_Text _playerRewardPoints;
        [SerializeField] private MyProfileScreen _myProfileScreen;
    
        [Header("Stars Info")]
        [SerializeField] private Sprite _starSprite;
        [SerializeField] private TMP_Text _playerStarsText;
        [SerializeField] private Image _starImage;
    
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
                _playerSlots[i].Init(_playersData[i], _starSprite);
            }
        }

        private void UpdatePlayerSlot()
        {
            foreach (var slot in _playerSlots)
            {
                if (!slot.IsPlayer) continue;

                string playerName = SaveSystem.Instance.LoadPlayerName();
                int rewardPoints = SaveSystem.Instance.LoadLastReward();
                int totalStars = SaveSystem.Instance.GetTotalStars(50);

                _playerName.text = playerName;
                _playerRewardPoints.text = rewardPoints.ToString();
                _playerStarsText.text = totalStars.ToString();
            
                if (_starImage != null)
                {
                    _starImage.sprite = _starSprite;
                }
            
                _myProfileScreen.LoadSavedAvatar();
                _playerAvatar = _myProfileScreen.AvatarImage;
            
                slot.SetPlayer(playerName, rewardPoints, _playerAvatar, totalStars);
            
                break;
            }
        }

        private void SortByRewardPoints()
        {
            List<PlayerSlot> sortedSlots = _playerSlots
                .OrderByDescending(slot => slot.TotalStars)
                .ToList();

            for (int i = 0; i < sortedSlots.Count; i++)
            {
                sortedSlots[i].transform.SetSiblingIndex(i);
            }
        }
    }
}