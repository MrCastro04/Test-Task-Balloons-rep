using System.Collections;
using Modules.Core.UI.Screens.Base_Screen;
using Modules.New;
using UnityEngine;

public class GameBoot : MonoBehaviour
{
    [SerializeField] private CanvasSystem _canvasSystem;
    [SerializeField] private BaseScreen[] _baseScreens;

    private IEnumerator Start()
    {
       yield return _canvasSystem.Init(_baseScreens);
        
        Debug.Log("Меню запущено!");

        yield return null;
    }
}