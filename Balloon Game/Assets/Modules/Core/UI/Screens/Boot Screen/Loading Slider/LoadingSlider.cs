using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.New
{
    public class LoadingSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        
        public IEnumerator RunLoading(float loadingTime)
        {
            _slider.value = 0f;
            
            float elapsedTime = 0f;

            while (elapsedTime < loadingTime)
            {
                elapsedTime += Time.deltaTime;
                
                _slider.value = elapsedTime / loadingTime;
                
                yield return null;
            }

            _slider.value = 1f;
        }
        
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

    }
}