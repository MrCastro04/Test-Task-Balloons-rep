using System.Collections;
using System.Collections.Generic;
using Modules.Core.UI.Screens.Base_Screen;
using TMPro;
using UnityEngine;

public class ShopScreenFirst : BaseScreen
{
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private List<BalloonBlockButton> _buyButtons;
    [SerializeField] private BalloonSkinSystem _balloonSkinSystem;
    
    public override IEnumerator Open()
    {
        UpdateReward();
        UpdateBuyTextPhoto();
        UpdateSelectedSkin();
        yield return base.Open();
    }
    
    private void UpdateSelectedSkin()
    {
        Sprite selectedSkin = _balloonSkinSystem.SelectedSkin;
        foreach (var button in _buyButtons)
        {
            button.UpdateSelectedHighlight(selectedSkin);
        }
    }

    private void UpdateBuyTextPhoto()
    {
        foreach (var button in _buyButtons)
        {
            if (_balloonSkinSystem.PlayerBalloonSkins.Contains(button.SkinOnThisButton))
            {
                button.RemoveBuyTextPhoto();
            }
        }
    }

    private void UpdateReward()
    {
        if (_rewardText != null)
        {
            int reward = SaveSystem.Instance.LoadLastReward();
            _rewardText.text = $"{reward}";
        }
    }
}