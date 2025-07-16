using System.Collections;
using UnityEngine;
using DG.Tweening;
using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.UI.Screens.Base_Screen;
using Modules.New;

public class BootScreen : BaseScreen
{
    [SerializeField] private BaseScreen _nextScreen;

    [Header("Loading Settings")]
    [SerializeField] private LoadingSlider _loadingSlider;
    [SerializeField] private float _loadingTime = 5f;

    [Header("Balloon Settings")]
    [SerializeField] private RectTransform[] _balloons;
    [SerializeField] private float _flyDurationToCenter = 1.5f;
    [SerializeField] private float _flyDurationToTop = 1.5f;
    [SerializeField] private float _waitAtCenter = 2f;

    private float[] _centerPositions; 

    public override IEnumerator Open()
    {
        _centerPositions = new float[_balloons.Length];
        
        foreach (var balloon in _balloons)
        {
            if(balloon.gameObject.activeSelf == false)
                continue;
            
            balloon.gameObject.SetActive(false);
        }
        
        for (int i = 0; i < _balloons.Length; i++)
        {
            _centerPositions[i] = UIPositionHelper.GetCanvasCenterPosition(_balloons[i]).y;

            RectTransform balloon = _balloons[i];
            Vector3 startPos = balloon.anchoredPosition;

            balloon.anchoredPosition = new Vector2(startPos.x, -Screen.height - 500);
            balloon.gameObject.SetActive(true);
        }

       
        Sequence flyToCenterSequence = DOTween.Sequence();
        for (int i = 0; i < _balloons.Length; i++)
        {
            RectTransform balloon = _balloons[i];
            flyToCenterSequence.Join(
                balloon.DOAnchorPosY(_centerPositions[i], _flyDurationToCenter)
                    .SetEase(Ease.OutCubic)
            );
        }
        yield return flyToCenterSequence.WaitForCompletion();

       
        yield return new WaitForSeconds(_waitAtCenter);
        yield return _loadingSlider.RunLoading(_loadingTime);

      
        Sequence flyToTopSequence = DOTween.Sequence();
        foreach (var balloon in _balloons)
        {
            flyToTopSequence.Join(
                balloon.DOAnchorPosY(Screen.height * 3, _flyDurationToTop)
                    .SetEase(Ease.InCubic)
            );
        }
        yield return flyToTopSequence.WaitForCompletion();
        
        yield return Exit();

        ActionSystem.Instance.Perform(new OpenScreenGA(_nextScreen));
    }

    protected override IEnumerator Exit()
    {
        foreach (var balloon in _balloons)
        {
            balloon.gameObject.SetActive(false);
        }
        
        _loadingSlider.gameObject.SetActive(false);
        
        return base.Exit();
    }
}
