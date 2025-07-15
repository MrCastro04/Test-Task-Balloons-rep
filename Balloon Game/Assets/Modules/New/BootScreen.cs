using System.Collections;
using Modules.New;
using UnityEngine;

public class BootScreen : MonoBehaviour 
{
    [SerializeField] private LoadingSlider _loadingSlider;

    public IEnumerator BootLoading( float loadingTime)
    {
       yield return _loadingSlider.RunLoading(loadingTime);
    }
}