using System.Collections;
using Modules.Core.UI.Screens.Base_Screen;
using TMPro;
using UnityEngine;

public class WinScreen : BaseScreen
{
    [SerializeField] private TMP_Text _scoreText;
    
    public void SetScore(int score)
    {
        if (_scoreText != null)
            _scoreText.text = $"{score}";
    }
}