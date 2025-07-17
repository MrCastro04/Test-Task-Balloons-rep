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
    [SerializeField] private List<BuyButton> _buyButtons;

    public override IEnumerator Open()
    {
        UpdateUI();
        UpdateReward();
        yield return base.Open();
    }

    private void OnEnable()
    {
        ActionSystem.SubscribeReaction<PlayerPurchaseBalloonGA>(OnPlayerPurchase, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<PlayerPurchaseBalloonGA>(OnPlayerPurchase, ReactionTiming.POST);
    }

    private void UpdateReward()
    {
        if (_rewardText != null)
        {
            int reward = SaveSystem.Instance.LoadLastReward();
            _rewardText.text = $"{reward}";
        }
    }

    private void UpdateUI()
    {
        foreach (var button in _buyButtons)
        {
            if (SaveSystem.Instance.IsSkinPurchased(button.BalloonIndex))
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    private void OnPlayerPurchase(PlayerPurchaseBalloonGA ga)
    {
        var button = _buyButtons.FirstOrDefault(b => b.BalloonIndex == ga.BalloonIndex);
        if (button != null)
            button.gameObject.SetActive(false);
        UpdateReward();
    }
}