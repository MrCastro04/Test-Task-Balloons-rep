
    using Modules.Core.Systems.Action_System.Scripts;
    using UnityEngine;

    public class BalloonBlockButton : BaseButton
    {
        [SerializeField] public int SkinId;
        [SerializeField] public Sprite SkinOnThisButton;
        [SerializeField] private BuyScreen _buyScreen;
        [SerializeField] private SelectScreen _selectScreen;

        protected override void OnClickAction()
        {
            ActionSystem.Instance.Perform(new PlayerTapBalloonBlockGA(SkinId,SkinOnThisButton,_buyScreen,_selectScreen));
        }
    }
