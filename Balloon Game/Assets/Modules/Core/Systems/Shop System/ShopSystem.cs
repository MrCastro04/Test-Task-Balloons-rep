using System.Collections;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.Utility.Singleton;

public class ShopSystem : Singleton<ShopSystem>
{
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

        ActionSystem.Instance.Perform(new PlayerPurchaseBalloonGA(ga.BalloonIndex, ga.BalloonSprite));
        yield return null;
    }
}