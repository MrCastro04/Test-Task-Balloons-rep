using System.Collections;
using Modules.Core.UI.Screens.Base_Screen;
using UnityEngine;

public class LevelScreen : BaseScreen
{
    [SerializeField] private LevelButton[] _levelButtons;

    public override IEnumerator Open()
    {
        UpdateAllLevels();
        yield return base.Open();
    }

    private void UpdateAllLevels()
    {
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            int stars = SaveSystem.Instance.LoadLevelStars(i + 1);
            _levelButtons[i].UpdateStars(stars);
        }
    }
}