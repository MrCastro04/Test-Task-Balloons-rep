using Modules.Core.UI.Screens.Base_Screen;
using TMPro;
using UnityEngine;

namespace Modules.Core.UI.Screens.Win_Screen
{
    public class WinScreen : BaseScreen
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _rewardText;
    
        public void SetScore(int score)
        {
            if (_scoreText != null)
                _scoreText.text = $"{score}";
        }
    
        public void SetReward(int reward)
        {
            if (_rewardText != null)
                _rewardText.text = $"{reward}";
        }
    }
}