using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButton : MonoBehaviour
{
    private Button _button;
    [SerializeField] private AudioClip _clickSound;

    protected virtual void Awake()
    {
        _button = gameObject.GetComponent<Button>();

        if (_button != null)
        {
            _button.onClick.RemoveAllListeners();

            _button.onClick.AddListener(OnClick);
        }
    }

    private void OnClick()
    {
        PlayClickSound();

        OnClickAction();
    }

    protected virtual void PlayClickSound()
    {
        if (_clickSound != null)
            SoundSystem.Instance.PlaySound(_clickSound);
    }

    protected abstract void OnClickAction();
}