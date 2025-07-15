using System.Collections;
using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.Systems.Interfaces;
using Modules.Core.UI.Screens.Base_Screen;
using Unity.VisualScripting;
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

        foreach (var screen in baseScreens)
        {
            if (screen == _baseScreens[0])
            {
                continue;
            }
            
            screen.gameObject.SetActive(false);
        }

        BaseScreen bootScreen = _baseScreens[0];

        OpenScreenGA openScreenBoot = new(bootScreen);

        ActionSystem.Instance.Perform(openScreenBoot);

        yield return new WaitUntil(() => ActionSystem.Instance.IsPerforming == false); 

        OpenScreenGA openScreenMenu = new(_baseScreens[1]);

        ActionSystem.Instance.Perform(openScreenMenu);
    }

    private IEnumerator OpenScreenPeformer(OpenScreenGA openScreenGa)
    {
        BaseScreen nextScreen = openScreenGa.NextScreen;
        
        _canvasGroup.alpha = 0f;

        yield return FadeIn();
        
        nextScreen.gameObject.SetActive(true);

        yield return nextScreen.Open();
    }

    private IEnumerator CloseScreenPerformer(CloseScreenGA closeScreenGa)
    {
        yield return FadeOut();

        int currentIndex = System.Array.IndexOf(_baseScreens, closeScreenGa.CurrentScreen);

        if (currentIndex > 0)
        {
            BaseScreen previousScreen = _baseScreens[currentIndex - 1];
            
            previousScreen.gameObject.SetActive(true);
            _canvasGroup.alpha = 0f;
            
            yield return FadeIn();
            yield return previousScreen.Open();
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