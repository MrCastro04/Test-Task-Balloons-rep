using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using UnityEngine;

public class SelectButton : BaseButton
{
    [SerializeField] private BalloonSkinSystem _balloonSkinSystem;
    [SerializeField] private SelectScreen _selectScreen;
    private Sprite _balloonSprite;

    protected override void OnClickAction()
    {
        _balloonSkinSystem.SetSelectedSkin(_balloonSprite); 
        
        ActionSystem.Instance.Perform(new CloseScreenGA(_selectScreen));
    }
}