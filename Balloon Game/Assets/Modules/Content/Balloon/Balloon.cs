using System;
using DG.Tweening;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private Tween _flyTween;
    private Tween _swayTween;
    private bool _isPopped;

    public event Action<Balloon> OnPopped;

    private void OnMouseDown()
    {
        Pop();
    }
    
    public void SetSkin(Sprite newSkin)
    {
        Debug.Log("Skin set");
        _spriteRenderer.sprite = newSkin;
    }

    public void FlyTo3D(Vector3 targetPos, float duration)
    {
        _isPopped = false;
        KillTweens();

        Vector3 startPos = transform.position;
        startPos.x += UnityEngine.Random.Range(-2f, 2f);
        startPos.z += UnityEngine.Random.Range(-2f, 2f);
        transform.position = startPos;

        _swayTween = transform
            .DOMoveX(startPos.x + UnityEngine.Random.Range(-0.5f, 0.5f), 1.5f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        _flyTween = transform
            .DOMoveY(targetPos.y, duration)
            .SetEase(Ease.InCubic)
            .OnComplete(() =>
            {
                ReturnToPool();
            });
    }

    public void Pop()
    {
        if (_isPopped) return;
        _isPopped = true;

        OnPopped?.Invoke(this);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        KillTweens();
        gameObject.SetActive(false);
    }

    private void KillTweens()
    {
        _flyTween?.Kill();
        _swayTween?.Kill();
    }

    private void OnDisable()
    {
        KillTweens();
    }
}