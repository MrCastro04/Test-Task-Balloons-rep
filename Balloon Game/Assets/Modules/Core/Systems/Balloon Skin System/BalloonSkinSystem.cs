using System.Collections;
using System.Collections.Generic;
using Modules.Core.Game_Actions;
using UnityEngine;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.Utility.Singleton;

public class BalloonSkinSystem : Singleton<BalloonSkinSystem>
{
    [SerializeField] private List<Sprite> allBalloonSkins;
    [SerializeField] private Sprite _defaultSkin;
    [SerializeField] private BuyScreen _buyScreen;
    [SerializeField] private SelectScreen _selectScreen;
    
    private List<Sprite> _playerBalloonSkins = new();
    private Sprite _selectedSkin;
    
    public List<Sprite> PlayerBalloonSkins => _playerBalloonSkins;
    public Sprite SelectedSkin => _selectedSkin;
    
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<PlayerTapBalloonBlockGA>(PlayerTapBalloonBlockPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<PlayerTapBalloonBlockGA>();
    }

    public Sprite GetSelectedOrDefaultSkin()
    {
        if (_selectedSkin == null)
            return _defaultSkin;
        return _selectedSkin;
    }

    private void LoadSelectedSkin()
    {
        int selectedSkinIndex = SaveSystem.Instance.LoadSelectedSkinIndex();
        if (selectedSkinIndex >= 0 && selectedSkinIndex < allBalloonSkins.Count)
            _selectedSkin = allBalloonSkins[selectedSkinIndex];
        else
            _selectedSkin = _defaultSkin;
    }

    public void SaveNewSkin (Sprite newSkin)
    {
        _playerBalloonSkins.Add(newSkin);
        _selectedSkin = newSkin;

        int skinIndex = allBalloonSkins.IndexOf(newSkin);
        
        SaveSystem.Instance.SaveSelectedSkinIndex(skinIndex);
    }

    private IEnumerator PlayerTapBalloonBlockPerformer(PlayerTapBalloonBlockGA playerTapBalloonBlockGa)
    {
        if (playerTapBalloonBlockGa.ID < 0 || playerTapBalloonBlockGa.ID >= allBalloonSkins.Count)
            yield break;
        
        Sprite tappedSkin = allBalloonSkins[playerTapBalloonBlockGa.ID];
        
        if (_playerBalloonSkins.Contains(tappedSkin))
        {
            yield return null;
            
            ActionSystem.Instance.AddReaction(new OpenScreenGA(playerTapBalloonBlockGa.SelectScreen));
        }
        else
        {
            yield return null;

            _buyScreen.Load(playerTapBalloonBlockGa.ID, playerTapBalloonBlockGa.BalloonBlockSkin);
            
            ActionSystem.Instance.AddReaction(new OpenScreenGA(playerTapBalloonBlockGa.BuyScreen));
        }
    }
}
