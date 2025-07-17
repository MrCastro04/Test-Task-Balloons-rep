using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.UI.Screens.Base_Screen;
using TMPro;
using UnityEngine;

public class ShopScreenSecond : BaseScreen
{
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private List<BalloonBlockButton> _buyButtons;

    public override IEnumerator Open()
    {
        UpdateReward();
        yield return base.Open();
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