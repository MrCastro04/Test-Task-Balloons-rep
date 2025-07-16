using UnityEngine;
using System.Collections;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.Systems.Interfaces;
using Modules.Core.Utility.Singleton;

public class SaveSystem : Singleton<SaveSystem>, ISystem
{
    private const string PlayerNameKey = "PlayerName";
    private const string SoundVolumeKey = "SoundVolume";
    private const string MusicVolumeKey = "MusicVolume";

    public void OnEnable()
    {
        ActionSystem.AttachPerformer<SavePlayerNameGA>(SavePlayerNamePerformer);
        ActionSystem.AttachPerformer<SaveSettingsGA>(SaveSettingsPerformer);
    }

    public void OnDisable()
    {
        ActionSystem.DetachPerformer<SavePlayerNameGA>();
        ActionSystem.DetachPerformer<SaveSettingsGA>();
    }

    private IEnumerator SavePlayerNamePerformer(SavePlayerNameGA savePlayerNameGa)
    {
        PlayerPrefs.SetString(PlayerNameKey, savePlayerNameGa.PlayerName);
        PlayerPrefs.Save();
        Debug.Log("Имя сохранено: " + savePlayerNameGa.PlayerName);
        
        yield return null;
    }

    private IEnumerator SaveSettingsPerformer(SaveSettingsGA saveSettingsGa)
    {
        PlayerPrefs.SetFloat(SoundVolumeKey, saveSettingsGa.SoundVolume);
        PlayerPrefs.SetFloat(MusicVolumeKey, saveSettingsGa.MusicVolume);
        PlayerPrefs.Save();
        Debug.Log($"Настройки сохранены: Sound={saveSettingsGa.SoundVolume}, Music={saveSettingsGa.MusicVolume}");
        yield return null;
    }

    public string LoadPlayerName()
    {
        if (PlayerPrefs.HasKey(PlayerNameKey) == false)
        {
            return "Player";
        }
        
        return PlayerPrefs.GetString(PlayerNameKey);
    }

    public float LoadSoundVolume()
    {
        if (PlayerPrefs.HasKey(SoundVolumeKey) == false)
        {
            return 0.5f;
        }
        
        return PlayerPrefs.GetFloat(SoundVolumeKey);
    }

    public float LoadMusicVolume()
    {
        if (PlayerPrefs.HasKey(MusicVolumeKey) == false)
        {
            return 0.5f;
        }

        return PlayerPrefs.GetFloat(MusicVolumeKey);
    }
}