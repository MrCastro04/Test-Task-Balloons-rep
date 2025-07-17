using System.Collections;
using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.Utility.Singleton;
using UnityEngine;

public class ShopSystem : Singleton<ShopSystem>
{
    [SerializeField] private BuyScreen _buyScreen;
    
    private const int BalloonCost = 1000;
    
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<BuyBalloonGA>(BuyBalloonPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<BuyBalloonGA>();
    }

    private IEnumerator BuyBalloonPerformer(BuyBalloonGA ga)
    {
        int currentCoins = SaveSystem.Instance.LoadLastReward();
        
        if (currentCoins < BalloonCost)
            yield break;

        SaveSystem.Instance.AddScoreAndReward(SaveSystem.Instance.LoadLastScore(), -BalloonCost);
        SaveSystem.Instance.SavePurchasedSkin(ga.BalloonIndex);
        yield return null;
    }
}