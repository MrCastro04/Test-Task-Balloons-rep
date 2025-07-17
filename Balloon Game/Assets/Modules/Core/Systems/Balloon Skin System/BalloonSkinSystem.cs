using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.Utility.Singleton;

public class BalloonSkinSystem : Singleton<BalloonSkinSystem>
{
    [SerializeField] private List<Sprite> allBalloonSkins;
    [SerializeField] private Sprite _defaultSkin;
    
    private List<Sprite> _playerBalloonSkins = new();
    private Sprite _selectedSkin;
    
    public List<Sprite> PlayerBalloonSkins => _playerBalloonSkins;
    public Sprite SelectedSkin => _selectedSkin;

    private void Start()
    {
        LoadSelectedSkin();
    }

    private void OnEnable()
    {
        ActionSystem.SubscribeReaction<PlayerPurchaseBalloonGA>(PlayerPurchaseBalloonReaction,ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<PlayerPurchaseBalloonGA>(PlayerPurchaseBalloonReaction,ReactionTiming.POST);
    }

    public Sprite GetSelectedOrDefaultSkin()
    {
        if (_selectedSkin == null)
        {
            return _defaultSkin;
        }

        return _selectedSkin;
    }

    private void LoadSelectedSkin()
    {
        int selectedSkinIndex = SaveSystem.Instance.LoadSelectedSkinIndex();
        
        if (selectedSkinIndex >= 0 && selectedSkinIndex < allBalloonSkins.Count)
        {
            _selectedSkin = allBalloonSkins[selectedSkinIndex];
        }
        else
        {
            _selectedSkin = _defaultSkin;
        }
    }

    private void PlayerPurchaseBalloonReaction(PlayerPurchaseBalloonGA ga)
    {
        if (!allBalloonSkins.Contains(ga.BalloonSprite))
            return;

        _playerBalloonSkins.Add(ga.BalloonSprite);
        _selectedSkin = ga.BalloonSprite;
        
        // Сохраняем индекс выбранного скина
        int skinIndex = allBalloonSkins.IndexOf(ga.BalloonSprite);
        SaveSystem.Instance.SaveSelectedSkinIndex(skinIndex);
    }
}