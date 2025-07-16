using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.Utility.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem : Singleton<LevelSystem>
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<LoadLevelSceneGA>(LoadLevelScenePerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<LoadLevelSceneGA>();
    }

    private System.Collections.IEnumerator LoadLevelScenePerformer(LoadLevelSceneGA ga)
    {
        Debug.Log($"Загружаем сцену уровня (синхронно) с индексом {ga.LevelIndex}");
        
        SceneManager.LoadScene(ga.LevelIndex);
        yield return null;
    }
}