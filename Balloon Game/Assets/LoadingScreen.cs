using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider _progressBar;

    private void Start()
    {
        StartCoroutine(LoadingCoroutine());
    }

    private IEnumerator LoadingCoroutine()
    {
        float loadingTime = 5f;
        float elapsedTime = 0f;

        while (elapsedTime < loadingTime)
        {
            elapsedTime += Time.deltaTime;
            
            float progress = elapsedTime / loadingTime;
            
            _progressBar.value = progress;
            
            yield return null;
        }

        _progressBar.value = 1f;
        
        Debug.Log("Вы перешли в главное меню!");

        GoToMenu();
    }

    private void GoToMenu()
    {
        Debug.Log("Главное меню запущено!");
    }
}