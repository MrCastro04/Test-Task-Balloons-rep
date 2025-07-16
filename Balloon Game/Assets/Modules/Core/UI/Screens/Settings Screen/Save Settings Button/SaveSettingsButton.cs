using Modules.Core.Systems.Action_System.Scripts;
using UnityEngine;
using UnityEngine.UI;

    public class SaveSettingsButton : BaseButton
    {
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;

        protected override void OnClickAction()
        {
            float sound = _soundSlider.value;
            
            float music = _musicSlider.value;
            
            ActionSystem.Instance.Perform(new SaveSettingsGA(sound, music));
        }
    }
