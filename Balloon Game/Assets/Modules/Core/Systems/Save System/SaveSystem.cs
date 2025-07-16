using UnityEngine;
using System.Collections;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.Systems.Interfaces;
using Modules.Core.Utility.Singleton;

public class SaveSystem : Singleton<SaveSystem>, ISystem
{
    private const string PlayerNameKey = "PlayerName";

    public void OnEnable()
    {
        ActionSystem.AttachPerformer<SavePlayerNameGA>(SavePlayerNamePerformer);
    }

    public void OnDisable()
    {
        ActionSystem.DetachPerformer<SavePlayerNameGA>();
    }

    private IEnumerator SavePlayerNamePerformer(SavePlayerNameGA savePlayerNameGa)
    {
        PlayerPrefs.SetString(PlayerNameKey, savePlayerNameGa.PlayerName);
        PlayerPrefs.Save();
        Debug.Log("Имя сохранено: " + savePlayerNameGa.PlayerName);
        yield return null;
    }

    public string LoadPlayerName()
    {
        return PlayerPrefs.GetString(PlayerNameKey, "Player");
    }
}