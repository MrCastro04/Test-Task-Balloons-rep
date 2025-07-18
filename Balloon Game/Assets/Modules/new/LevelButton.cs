using UnityEngine;
using UnityEngine.UI;

namespace Modules.@new
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private Image[] _stars;
        [SerializeField] private Button _button;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Sprite _lockedSprite;
        [SerializeField] private Sprite _unlockedSprite;
        [SerializeField] private GameObject _lockObject;
    
        private void Awake()
        {
            if (_button == null)
                _button = GetComponent<Button>();
        
            if (_buttonImage == null)
                _buttonImage = GetComponent<Image>();
        }

        public void UpdateStars(int stars)
        {
            for (int i = 0; i < _stars.Length; i++)
            {
                _stars[i].enabled = i < stars;
            }
        }

        public void UpdateLockState(bool isUnlocked)
        {
            _button.interactable = isUnlocked;
            _buttonImage.raycastTarget = isUnlocked;
        
            if (_lockedSprite != null && _unlockedSprite != null)
            {
                _buttonImage.sprite = isUnlocked ? _unlockedSprite : _lockedSprite;
            }
        
            if (_lockObject != null)
            {
                _lockObject.SetActive(isUnlocked);
            }
        }
    }
}