using System.Collections;
using Modules.Core.UI.Screens.Base_Screen;
using Modules.New;
using UnityEngine;

public class BootScreen : BaseScreen
{
    [SerializeField] private LoadingSlider _loadingSlider;
    [SerializeField] private float _loadingTime;

    public override IEnumerator Open()
     {
         yield return _loadingSlider.RunLoading(_loadingTime);

         yield return base.Open();
     }

    protected override IEnumerator Exit()
    {
        yield return null;
    }
}