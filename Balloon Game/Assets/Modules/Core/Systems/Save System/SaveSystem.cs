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
    private const string LastScoreKey = "LastScore";
    private const string LastRewardKey = "LastReward";
    private const int DefaultReward = 1000;

    public void OnEnable()
    {
        ActionSystem.AttachPerformer<SavePlayerNameGA>(SavePlayerNamePerformer);
        ActionSystem.AttachPerformer<SaveSettingsGA>(SaveSettingsPerformer);
        InitializeDefaultReward();
    }

    public void OnDisable()
    {
        ActionSystem.DetachPerformer<SavePlayerNameGA>();
        ActionSystem.DetachPerformer<SaveSettingsGA>();
    }

    private void InitializeDefaultReward()
    {
        if (!PlayerPrefs.HasKey(LastRewardKey))
        {
            PlayerPrefs.SetInt(LastRewardKey, DefaultReward);
            PlayerPrefs.Save();
        }
    }
    
    public void SavePurchasedSkin(int index)
    {
        string key = $"PurchasedSkin_{index}";
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
    }

    public bool IsSkinPurchased(int index)
    {
        string key = $"PurchasedSkin_{index}";
        return PlayerPrefs.HasKey(key) && PlayerPrefs.GetInt(key) == 1;
    }


    public void AddScoreAndReward(int score, int rewardToAdd)
    {
        PlayerPrefs.SetInt(LastScoreKey, score);

        int currentReward = LoadLastReward();
        int newReward = currentReward + rewardToAdd;
        PlayerPrefs.SetInt(LastRewardKey, newReward);

        PlayerPrefs.Save();
    }


    public int LoadLastScore()
    {
        return PlayerPrefs.HasKey(LastScoreKey) ? PlayerPrefs.GetInt(LastScoreKey) : 0;
    }

    public int LoadLastReward()
    {
        return PlayerPrefs.HasKey(LastRewardKey) ? PlayerPrefs.GetInt(LastRewardKey) : DefaultReward;
    }

    public string LoadPlayerName()
    {
        if (!PlayerPrefs.HasKey(PlayerNameKey))
            return "Player";
        return PlayerPrefs.GetString(PlayerNameKey);
    }

    public float LoadSoundVolume()
    {
        if (!PlayerPrefs.HasKey(SoundVolumeKey))
            return 0.5f;
        return PlayerPrefs.GetFloat(SoundVolumeKey);
    }

    public float LoadMusicVolume()
    {
        if (!PlayerPrefs.HasKey(MusicVolumeKey))
            return 0.5f;
        return PlayerPrefs.GetFloat(MusicVolumeKey);
    }

    private IEnumerator SavePlayerNamePerformer(SavePlayerNameGA savePlayerNameGa)
    {
        PlayerPrefs.SetString(PlayerNameKey, savePlayerNameGa.PlayerName);
        PlayerPrefs.Save();
        yield return null;
    }

    private IEnumerator SaveSettingsPerformer(SaveSettingsGA saveSettingsGa)
    {
        PlayerPrefs.SetFloat(SoundVolumeKey, saveSettingsGa.SoundVolume);
        PlayerPrefs.SetFloat(MusicVolumeKey, saveSettingsGa.MusicVolume);
        PlayerPrefs.Save();
        yield return null;
    }
}
