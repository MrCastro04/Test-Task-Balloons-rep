using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.Utility.Singleton;

public class BalloonSkinSystem : Singleton<BalloonSkinSystem>
{
    [SerializeField] private List<Sprite> allBalloonSkins;
    
    private List<Sprite> _playerBalloonSkins = new();
    private Sprite _selectedSkin;
    
    public List<Sprite> PlayerBalloonSkins => _playerBalloonSkins;

    public Sprite SelectedSkin => _selectedSkin;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<PlayerPurchaseBalloonGA>(PlayerPurchaseBalloonPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<PlayerPurchaseBalloonGA>();
    }

    private IEnumerator PlayerPurchaseBalloonPerformer(PlayerPurchaseBalloonGA ga)
    {
        if (!allBalloonSkins.Contains(ga.BalloonSprite))
            yield break;

        _playerBalloonSkins.Add(ga.BalloonSprite);
        _selectedSkin = ga.BalloonSprite;
        yield return null;
    }
}