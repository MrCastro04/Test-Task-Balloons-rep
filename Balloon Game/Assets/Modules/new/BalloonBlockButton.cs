using Modules.Core.Systems.Action_System.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class BalloonBlockButton : BaseButton
{
    [SerializeField] public int SkinId;
    [SerializeField] public Sprite SkinOnThisButton;
    [SerializeField] private BuyScreen _buyScreen;
    [SerializeField] private SelectScreen _selectScreen;
    [SerializeField] private Image _buyTextPhoto;
    [SerializeField] private Image _selectedHighlight;

    public void RemoveBuyTextPhoto() => _buyTextPhoto.gameObject.SetActive(false);

    public void UpdateSelectedHighlight(Sprite selectedSkin)
    {
        if (_selectedHighlight != null)
            _selectedHighlight.gameObject.SetActive(selectedSkin == SkinOnThisButton);
    }

    protected override void OnClickAction()
    {
        ActionSystem.Instance.Perform(new PlayerTapBalloonBlockGA(SkinId, SkinOnThisButton, _buyScreen, _selectScreen));
    }
}