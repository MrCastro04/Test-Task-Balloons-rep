using System.Collections;
using UnityEngine;

public class GameBoot : MonoBehaviour
{
    [SerializeField] private BootScreen bootScreen;
    [SerializeField] private int _loadingTime;

    private IEnumerator Start()
    {
        yield return bootScreen.BootLoading(_loadingTime);
    }
}