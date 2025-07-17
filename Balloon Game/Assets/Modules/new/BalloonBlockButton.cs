
    using Modules.Core.Systems.Action_System.Scripts;
    using UnityEngine;

    public class BalloonBlockButton : BaseButton
    {
        [SerializeField] private int _skinId;
        [SerializeField] private Sprite _skinOnThisButton;
        [SerializeField] private BuyScreen _buyScreen;
        [SerializeField] private SelectScreen _selectScreen;
        
        protected override void OnClickAction()
        {
            ActionSystem.Instance.Perform(new PlayerTapBalloonBlockGA(_skinId,_skinOnThisButton,_buyScreen,_selectScreen));
        }
    }
