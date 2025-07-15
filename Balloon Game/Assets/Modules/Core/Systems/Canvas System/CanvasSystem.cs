using System.Collections;
using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.UI.Screens.Base_Screen;
using Modules.New;
using UnityEngine;

public class CanvasSystem : MonoBehaviour, ISystem
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeSpeed = 3f;

    private BaseScreen[] _baseScreens;

    public void OnEnable()
    {
        ActionSystem.AttachPerformer<OpenScreenGA>(OpenScreenPeformer);
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

        BaseScreen bootScreen = _baseScreens[0];

        yield return FadeIn();

        yield return bootScreen.Open();

        yield return FadeOut();

        OpenScreenGA openScreenGa = new(_baseScreens[1]);

        ActionSystem.Instance.Perform(openScreenGa);
    }

    private IEnumerator OpenScreenPeformer(OpenScreenGA openScreenGa)
    {
        yield return FadeIn();

        yield return openScreenGa.NextScreen.Open();
    }

    private IEnumerator CloseScreenPerformer(CloseScreenGA closeScreenGa)
    {
        yield return FadeOut();

        int currentIndex = System.Array.IndexOf(_baseScreens, closeScreenGa.CurrentScreen);

        if (currentIndex > 0)
        {
            BaseScreen previousScreen = _baseScreens[currentIndex - 1];

            OpenScreenGA openScreenGa = new(previousScreen);

            ActionSystem.Instance.Perform(openScreenGa);
        }
    }

    private IEnumerator FadeIn()
    {
        _canvasGroup.interactable = true;

        _canvasGroup.blocksRaycasts = true;

        while (_canvasGroup.alpha < 1f)
        {
            _canvasGroup.alpha += Time.deltaTime * _fadeSpeed;
            yield return null;
        }

        _canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOut()
    {
        _canvasGroup.interactable = false;

        _canvasGroup.blocksRaycasts = false;

        while (_canvasGroup.alpha > 0f)
        {
            _canvasGroup.alpha -= Time.deltaTime * _fadeSpeed;
            yield return null;
        }

        _canvasGroup.alpha = 0f;
    }
}