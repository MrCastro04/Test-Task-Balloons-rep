using System.Collections;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.Utility.Singleton;
using UnityEngine;
using TMPro;

public class GameplaySystem : Singleton<GameplaySystem>
{
    [Header("Level Settings")]
    [SerializeField] private BalloonPool _balloonPool;
    [SerializeField] private float _levelTimer = 30f;
    [SerializeField] private float _flyDuration = 3f;
    [SerializeField] private float _spawnOffset = 50f;
    [SerializeField] private int _targetScore = 30;

    [Header("Spawn Area")]
    [SerializeField] private RectTransform _spawnArea;

    [Header("UI")]
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _balloonsLeftText;

    private float _currentTime;
    private int _currentScore;
    private int _balloonsLeft;
    private bool _isRunning;
    private Coroutine _levelRoutine;

    private void OnEnable()
    {
        ActionSystem.SubscribeReaction<StartLevelGA>(StartLevelReaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<StartLevelGA>(StartLevelReaction, ReactionTiming.POST);
    }

    private void StartLevelReaction(StartLevelGA ga)
    {
        if (_isRunning) return;

        Debug.Log("Level started!");
        _isRunning = true;
        _currentTime = _levelTimer;
        _currentScore = 0;

        _balloonPool.PopulatePool(30); // Количество шаров в пуле
        _balloonsLeft = 30;

        UpdateTimerUI();
        UpdateScoreUI();
        UpdateBalloonsLeftUI();

        _levelRoutine = StartCoroutine(LevelTimerRoutine());
    }

    private IEnumerator LevelTimerRoutine()
    {
        while (_currentTime > 0)
        {
            SpawnBalloon();
            yield return new WaitForSeconds(1f);

            _currentTime--;
            UpdateTimerUI();
        }

        EndLevel();
    }

    private void SpawnBalloon()
    {
        var balloon = _balloonPool.GetBalloon();
        if (balloon == null) return; // Пул пуст

        _balloonsLeft--;
        UpdateBalloonsLeftUI();

        balloon.transform.SetParent(_spawnArea, false);

        float halfWidth = _spawnArea.rect.width * 0.5f;
        float halfHeight = _spawnArea.rect.height * 0.5f;

        float startX = Random.Range(-halfWidth, halfWidth);
        float startY = -halfHeight - _spawnOffset;

        balloon.GetComponent<RectTransform>().anchoredPosition = new Vector2(startX, startY);

        float endY = halfHeight + _spawnOffset;

        balloon.OnPopped += OnBalloonPopped;
        balloon.Fly(endY, _flyDuration);
    }

    private void OnBalloonPopped(Balloon balloon)
    {
        balloon.OnPopped -= OnBalloonPopped;
        _currentScore++;
        UpdateScoreUI();
    }

    private void EndLevel()
    {
        Debug.Log($"Level Finished! Final Score: {_currentScore}/{_targetScore}");
        _isRunning = false;

        if (_levelRoutine != null)
        {
            StopCoroutine(_levelRoutine);
            _levelRoutine = null;
        }

        if (_currentScore >= _targetScore)
            Debug.Log("You WIN!");
        else
            Debug.Log("You LOSE!");
    }

    private void UpdateTimerUI()
    {
        if (_timerText != null)
            _timerText.text = $"Time: {_currentTime:0}";
    }

    private void UpdateScoreUI()
    {
        if (_scoreText != null)
            _scoreText.text = $"Score: {_currentScore}/{_targetScore}";
    }

    private void UpdateBalloonsLeftUI()
    {
        if (_balloonsLeftText != null)
            _balloonsLeftText.text = $"Balloons left: {_balloonsLeft}";
    }
}
