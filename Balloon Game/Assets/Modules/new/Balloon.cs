using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class Balloon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _balloonImage;
    [SerializeField] private Color[] _balloonColors;

    private RectTransform _rectTransform;
    private Tween _flyTween;
    private Tween _swayTween;
    private bool _isPopped;

    public event Action<Balloon> OnPopped;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Fly(float endY, float duration)
    {
        _isPopped = false;
        KillTweens();
        SetRandomColor();

        float startX = UnityEngine.Random.Range(-200f, 200f);
        _rectTransform.anchoredPosition = new Vector2(startX, _rectTransform.anchoredPosition.y);

        _swayTween = _rectTransform
            .DOAnchorPosX(startX + UnityEngine.Random.Range(-50f, 50f), 1.5f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        _flyTween = _rectTransform
            .DOAnchorPosY(endY, duration)
            .SetEase(Ease.InCubic)
            .OnComplete(DestroyBalloon); // Улетел – уничтожаем
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PopBalloon();
    }

    private void PopBalloon()
    {
        if (_isPopped) return;
        _isPopped = true;

        Debug.Log("Balloon popped!");
        DestroyBalloon();
    }

    private void DestroyBalloon()
    {
        KillTweens();
        OnPopped?.Invoke(this);
        gameObject.SetActive(false);
        Destroy(gameObject, 0.05f);
    }

    private void SetRandomColor()
    {
        if (_balloonColors.Length > 0)
            _balloonImage.color = _balloonColors[UnityEngine.Random.Range(0, _balloonColors.Length)];
    }

    private void KillTweens()
    {
        _flyTween?.Kill();
        _swayTween?.Kill();
    }
}