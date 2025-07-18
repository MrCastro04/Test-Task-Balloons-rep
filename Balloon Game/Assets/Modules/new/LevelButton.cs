using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Image[] _stars;

    public void UpdateStars(int stars)
    {
        for (int i = 0; i < _stars.Length; i++)
        {
            _stars[i].enabled = i < stars;
        }
    }
}