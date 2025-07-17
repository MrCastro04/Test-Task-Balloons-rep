using System;
using DG.Tweening;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private Tween _flyTween;
    private Tween _swayTween;
    private bool _isPopped;

    public event Action<Balloon> OnPopped;

    private void OnMouseDown()
    {
        Pop();
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
                KillTweens();
                Destroy(gameObject);
            }); 
    }

    public void Pop()
    {
        if (_isPopped) return;
        _isPopped = true;

        Debug.Log($"Balloon popped: {gameObject.name}");
        DestroyBalloon();
    }

    private void DestroyBalloon()
    {
        KillTweens();
        OnPopped?.Invoke(this);
        gameObject.SetActive(false);
        Destroy(gameObject, 0.05f);
    }

    private void KillTweens()
    {
        _flyTween?.Kill();
        _swayTween?.Kill();
    }
}