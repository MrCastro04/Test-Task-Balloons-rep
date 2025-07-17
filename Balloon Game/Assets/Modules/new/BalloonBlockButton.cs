
    using Modules.Core.Systems.Action_System.Scripts;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Image = UnityEngine.UI.Image;

    public class BalloonBlockButton : BaseButton
    {
        [SerializeField] public int SkinId;
        [SerializeField] public Sprite SkinOnThisButton;
        [SerializeField] private BuyScreen _buyScreen;
        [SerializeField] private SelectScreen _selectScreen;
        [SerializeField] private Image _buyTextPhoto;

        public void RemoveBuyTextPhoto() => _buyTextPhoto.gameObject.SetActive(false);
        
        protected override void OnClickAction()
        {
            ActionSystem.Instance.Perform(new PlayerTapBalloonBlockGA(SkinId,SkinOnThisButton,_buyScreen,_selectScreen));
        }
    }
