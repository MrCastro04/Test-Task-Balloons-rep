using System.Collections;
using UnityEngine;
using DG.Tweening;
using Modules.Core.UI.Screens.Base_Screen;

namespace Modules.New
{
    public class MainMenuScreen : BaseScreen
    {
        [SerializeField] private RectTransform[] _balloons;
        [SerializeField] private float _flyDurationToTarget = 1.5f;
        [SerializeField] private float _balloonsAmplitude = 30f;
        [SerializeField] private float _balloonsDuration = 2f;
        [SerializeField] private float _extraHeight = 100f;

        public override IEnumerator Open()
        {
            foreach (var balloon in _balloons)
            {
                Vector2 startPos = balloon.anchoredPosition;
                balloon.anchoredPosition = new Vector2(startPos.x, -Screen.height - 500);
                balloon.gameObject.SetActive(true);
            }

            Sequence flySequence = DOTween.Sequence();
            foreach (var balloon in _balloons)
            {
                float centerY = UIPositionHelper.GetCanvasCenterPosition(balloon).y;
                float targetY = centerY + _extraHeight;
                flySequence.Join(
                    balloon.DOAnchorPosY(targetY, _flyDurationToTarget)
                        .SetEase(Ease.OutCubic)
                );
            }
            yield return flySequence.WaitForCompletion();

            foreach (var balloon in _balloons)
            {
                float startY = balloon.anchoredPosition.y;
                balloon.DOAnchorPosY(startY + _balloonsAmplitude, _balloonsDuration)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(-1, LoopType.Yoyo);
            }
        }

        protected override IEnumerator Exit()
        {
            foreach (var balloon in _balloons)
            {
                balloon.DOKill();
                balloon.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}