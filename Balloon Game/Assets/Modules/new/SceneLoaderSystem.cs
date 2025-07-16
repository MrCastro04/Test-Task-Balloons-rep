
    using System.Collections;
    using Modules.Core.Systems.Action_System.Scripts;
    using Modules.Core.Utility.Singleton;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class SceneLoaderSystem : Singleton<SceneLoaderSystem>
    {
        private void OnEnable()
        {
            ActionSystem.AttachPerformer<LoadLevelSceneGA>(LoadLevelScenePerformer);
        }

        private void OnDisable()
        {
            ActionSystem.DetachPerformer<LoadLevelSceneGA>();
        }

        private IEnumerator LoadLevelScenePerformer(LoadLevelSceneGA ga)
        {
            SceneManager.LoadScene(ga.LevelIndex);
            yield return null;
        }
    }
