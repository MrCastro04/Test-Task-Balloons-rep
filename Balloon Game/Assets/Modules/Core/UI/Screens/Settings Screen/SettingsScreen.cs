using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Modules.Core.UI.Screens.Base_Screen;

namespace Modules
{
    public class SettingsScreen : BaseScreen
    {
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;

        public override IEnumerator Open()
        {
            _soundSlider.value = SaveSystem.Instance.LoadSoundVolume();
            _musicSlider.value = SaveSystem.Instance.LoadMusicVolume();
            yield return null;
        }

        protected override IEnumerator Exit()
        {
            yield return null;
        }
    }
}