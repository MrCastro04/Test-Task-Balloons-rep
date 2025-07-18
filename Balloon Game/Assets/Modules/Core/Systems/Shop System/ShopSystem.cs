using System.Collections;
using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.Systems.Balloon_Skin_System;
using Modules.Core.Systems.Save_System;
using Modules.Core.Utility.Singleton;
using UnityEngine;

namespace Modules.Core.Systems.Shop_System
{
    public class ShopSystem : Singleton<ShopSystem>
    {
        [SerializeField] private BalloonSkinSystem _balloonSkinSystem;
    
        private const int BalloonCost = 1000;
    
        private void OnEnable()
        {
            ActionSystem.AttachPerformer<BuyBalloonGA>(BuyBalloonPerformer);
        }

        private void OnDisable()
        {
            ActionSystem.DetachPerformer<BuyBalloonGA>();
        }

        private IEnumerator BuyBalloonPerformer(BuyBalloonGA buyBalloonGa)
        {
            int currentCoins = SaveSystem.Instance.LoadLastReward();
        
            if (currentCoins < BalloonCost)
                yield break;

            SaveSystem.Instance.AddScoreAndReward(SaveSystem.Instance.LoadLastScore(), -BalloonCost);
            SaveSystem.Instance.SavePurchasedSkin(buyBalloonGa.BalloonIndex);

            _balloonSkinSystem.SaveNewSkin(buyBalloonGa.BalloonSprite);
        
            yield return null;
        }
    }
}