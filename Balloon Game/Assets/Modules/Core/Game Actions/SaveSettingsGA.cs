using Modules.Core.Systems.Action_System.Scripts;

namespace Modules.Core.Game_Actions
{
    public class SaveSettingsGA : GameAction
    {
        public readonly float SoundVolume;
        public readonly float MusicVolume;

        public SaveSettingsGA(float soundVolume, float musicVolume)
        {
            SoundVolume = soundVolume;
            MusicVolume = musicVolume;
        }
    }
}