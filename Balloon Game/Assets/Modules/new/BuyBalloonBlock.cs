using Modules.Core.Systems.Action_System.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class BuyBalloonBlock : BaseButton
{
    [SerializeField] private int _skinId;
    [SerializeField] private Sprite _skinSprite;
    [SerializeField] private Image _skinImage;

    public int SkinId
    {
        get => _skinId;
        set => _skinId = value;
    }

    public Sprite SkinSprite
    {
        get => _skinSprite;
        set
        {
            _skinSprite = value;
            if (_skinImage != null)
                _skinImage.sprite = _skinSprite;
        }
    }

    protected override void OnClickAction()
    {
        ActionSystem.Instance.Perform(new BuyBalloonGA(_skinId, _skinSprite));
    }
}