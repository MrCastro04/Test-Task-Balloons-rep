using System.Collections;
using System.Collections.Generic;
using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.Systems.Save_System;
using Modules.Core.UI.Screens.Buy_Screen;
using Modules.Core.UI.Screens.Select_Screen;
using Modules.Core.Utility.Singleton;
using UnityEngine;

namespace Modules.Core.Systems.Balloon_Skin_System
{
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

        private void Start()
        {
            LoadPurchasedSkins();
            LoadSelectedSkin();
        }

        public void SetSelectedSkin(Sprite skin) => _selectedSkin = skin;

        private void LoadPurchasedSkins()
        {
            _playerBalloonSkins.Clear();

            for (int i = 0; i < allBalloonSkins.Count; i++)
            {
                if (SaveSystem.Instance.IsSkinPurchased(i))
                {
                    _playerBalloonSkins.Add(allBalloonSkins[i]);
                    Debug.Log($"[BalloonSkinSystem] Загружен купленный скин: {allBalloonSkins[i].name}");
                }
            }
        }

        public Sprite GetSelectedOrDefaultSkin()
        {
            return _selectedSkin == null ? _defaultSkin : _selectedSkin;
        }

        private void LoadSelectedSkin()
        {
            int selectedSkinIndex = SaveSystem.Instance.LoadSelectedSkinIndex();
            if (selectedSkinIndex >= 0 && selectedSkinIndex < allBalloonSkins.Count)
                _selectedSkin = allBalloonSkins[selectedSkinIndex];
            else
                _selectedSkin = _defaultSkin;
        }

        public void SaveNewSkin(Sprite newSkin)
        {
            if (!_playerBalloonSkins.Contains(newSkin))
                _playerBalloonSkins.Add(newSkin);

            _selectedSkin = newSkin;

            int skinIndex = allBalloonSkins.IndexOf(newSkin);
            SaveSystem.Instance.SavePurchasedSkin(skinIndex);
            SaveSystem.Instance.SaveSelectedSkinIndex(skinIndex);
        }

        private IEnumerator PlayerTapBalloonBlockPerformer(PlayerTapBalloonBlockGA playerTapBalloonBlockGa)
        {
            if (playerTapBalloonBlockGa.ID < 0 || playerTapBalloonBlockGa.ID >= allBalloonSkins.Count)
                yield break;

            Sprite tappedSkin = allBalloonSkins[playerTapBalloonBlockGa.ID];

            if (_playerBalloonSkins.Contains(tappedSkin))
            {
                _selectScreen.Load(playerTapBalloonBlockGa.ID, playerTapBalloonBlockGa.BalloonBlockSkin);
                yield return null;
                ActionSystem.Instance.AddReaction(new OpenScreenGA(playerTapBalloonBlockGa.SelectScreen));
            }
            else
            {
                _buyScreen.Load(playerTapBalloonBlockGa.ID, playerTapBalloonBlockGa.BalloonBlockSkin);
                yield return null;
                ActionSystem.Instance.AddReaction(new OpenScreenGA(playerTapBalloonBlockGa.BuyScreen));
            }
        }
    }
}
