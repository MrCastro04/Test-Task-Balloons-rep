using System.Collections;
using UnityEngine;
using DG.Tweening;
using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.Systems.Interfaces;
using Modules.Core.UI.Screens.Base_Screen;

public class CanvasSystem : MonoBehaviour, ISystem
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeSpeed = 0.5f;

    private BaseScreen[] _baseScreens;
    private BaseScreen _currentScreen;
    private BaseScreen _previousScreen;

    public void OnEnable()
    {
        ActionSystem.AttachPerformer<OpenScreenGA>(OpenScreenPerformer);
        ActionSystem.AttachPerformer<CloseScreenGA>(CloseScreenPerformer);
    }

    public void OnDisable()
    {
        ActionSystem.DetachPerformer<OpenScreenGA>();
        ActionSystem.DetachPerformer<CloseScreenGA>();
    }

    public IEnumerator Init(BaseScreen[] baseScreens)
    {
        _baseScreens = baseScreens;

        foreach (var screen in _baseScreens)
            screen.gameObject.SetActive(false);

        _currentScreen = _baseScreens[0];
        _previousScreen = null;

        _currentScreen.gameObject.SetActive(true);
        _canvasGroup.alpha = 0f;

        yield return FadeIn();
        yield return _currentScreen.Open();
    }

    private IEnumerator OpenScreenPerformer(OpenScreenGA openScreenGa)
    {
        BaseScreen nextScreen = openScreenGa.NextScreen;

        if (_currentScreen == nextScreen)
            yield break;

        _previousScreen = _currentScreen;
        _currentScreen = nextScreen;

        yield return FadeOut();

        if (_previousScreen != null)
            _previousScreen.gameObject.SetActive(false);

        _currentScreen.gameObject.SetActive(true);
        _canvasGroup.alpha = 0f;

        yield return FadeIn();
        yield return _currentScreen.Open();
    }

    private IEnumerator CloseScreenPerformer(CloseScreenGA closeScreenGa)
    {
        if (_previousScreen == null)
            yield break;

        yield return FadeOut();

        _currentScreen.gameObject.SetActive(false);

        var temp = _currentScreen;
        _currentScreen = _previousScreen;
        _previousScreen = temp;

        _currentScreen.gameObject.SetActive(true);
        _canvasGroup.alpha = 0f;

        yield return FadeIn();
        yield return _currentScreen.Open();
    }

    private IEnumerator FadeIn()
    {
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        yield return _canvasGroup.DOFade(1f, _fadeSpeed).WaitForCompletion();
    }

    private IEnumerator FadeOut()
    {
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        yield return _canvasGroup.DOFade(0f, _fadeSpeed).WaitForCompletion();
    }
}
