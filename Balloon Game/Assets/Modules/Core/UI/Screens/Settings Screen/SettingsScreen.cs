using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Modules.Core.UI.Screens.Base_Screen;

namespace Modules
{
    public class SettingsScreen : BaseScreen
    {
        public static SettingsScreen Instance { get; private set; }

        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;

        private void Awake()
        {
            Instance = this;
        }

        public override IEnumerator Open()
        {
            _soundSlider.value = SaveSystem.Instance.LoadSoundVolume();
            _musicSlider.value = SaveSystem.Instance.LoadMusicVolume();
            yield return null;
        }

        public void UpdateSliders(float soundValue, float musicValue)
        {
            _soundSlider.value = soundValue;
            _musicSlider.value = musicValue;
        }

        protected override IEnumerator Exit()
        {
            yield return null;
        }
    }
}