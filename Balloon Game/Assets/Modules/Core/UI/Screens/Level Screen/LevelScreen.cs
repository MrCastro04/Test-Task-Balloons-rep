using System.Collections;
using Modules.Core.Systems.Save_System;
using Modules.Core.UI.Screens.Base_Screen;
using Modules.@new;
using UnityEngine;

namespace Modules.Core.UI.Screens.Level_Screen
{
    public class LevelScreen : BaseScreen
    {
        [SerializeField] private LevelButton[] _levelButtons;
        [SerializeField] private int _starsPerLevel = 3;

        public override IEnumerator Open()
        {
            UpdateAllLevels();
            yield return base.Open();
        }

        private void UpdateAllLevels()
        {
            int totalStars = GetTotalStars();
        
            for (int i = 0; i < _levelButtons.Length; i++)
            {
                int levelStars = SaveSystem.Instance.LoadLevelStars(i + 1);
                _levelButtons[i].UpdateStars(levelStars);
            
                bool isUnlocked = IsLevelUnlocked(i, totalStars);
                _levelButtons[i].UpdateLockState(isUnlocked);
            }
        }

        private int GetTotalStars()
        {
            return SaveSystem.Instance.GetTotalStars(_levelButtons.Length);
        }

        private bool IsLevelUnlocked(int levelIndex, int totalStars)
        {
            if (levelIndex == 0)
                return true;
        
            int starsRequired = levelIndex * _starsPerLevel;
            return totalStars >= starsRequired;
        }
    }
}