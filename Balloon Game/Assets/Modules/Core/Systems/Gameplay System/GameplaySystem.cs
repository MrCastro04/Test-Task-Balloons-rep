using System;
using System.Collections;
using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.Utility.Singleton;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class GameplaySystem : Singleton<GameplaySystem>
{
    [Header("WIN/LOSE Settings")] [SerializeField]
    private WinScreen _winScreen;

    [SerializeField] private LoseScreen _loseScreen;

    [Header("Level Settings")] [SerializeField]
    private BalloonPool _balloonPool;

    [SerializeField] private float _levelTimer = 30f;
    [SerializeField] private float _flyDuration = 3f;
    [SerializeField] private float _spawnOffset = 5f;
    [SerializeField] private int _targetScore = 15;
    [SerializeField] private int _levelIndex = 1;

    [Header("Spawn Area (3D)")] [SerializeField]
    private Transform _spawnAreaCenter;

    [SerializeField] private Vector3 _spawnAreaSize = new Vector3(10f, 0f, 10f);

    [Header("UI")] [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _scoreText;

    private float _currentTime;
    private int _currentScore;
    private bool _isRunning;
    private Coroutine _levelRoutine;
    private Sprite _balloonsSkin;
    private bool _levelEnd = false;

    private void OnEnable()
    {
        ActionSystem.SubscribeReaction<StartLevelGA>(OnStartLevel, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<EndLevelGA>(OnEndLevel, ReactionTiming.PRE);
    }

    private void OnDisable()
    {
        if (this != null)
        {
            ActionSystem.UnsubscribeReaction<StartLevelGA>(OnStartLevel, ReactionTiming.PRE);
            ActionSystem.UnsubscribeReaction<EndLevelGA>(OnEndLevel, ReactionTiming.PRE);
        }

        if (_levelRoutine != null)
        {
            StopCoroutine(_levelRoutine);
            _levelRoutine = null;
        }

        _isRunning = false;
    }

    private void OnStartLevel(StartLevelGA startLevelGa)
    {
        if (this == null || _isRunning) return;

        StartCoroutine(StartLevelCoroutine(startLevelGa));
    }

    private IEnumerator StartLevelCoroutine(StartLevelGA startLevelGa)
    {
        _balloonsSkin = BalloonSkinSystem.Instance.GetSelectedOrDefaultSkin();
        _isRunning = true;
        _currentTime = _levelTimer;
        _currentScore = 0;

        _balloonPool.PopulatePool(30, _balloonsSkin);

        UpdateTimerUI();
        UpdateScoreUI();

        if (_levelRoutine != null)
            StopCoroutine(_levelRoutine);

        _levelRoutine = StartCoroutine(LevelTimerRoutine());
        
        yield return null;
    }

    private void OnEndLevel(EndLevelGA endLevelGa)
    {
        if (this == null) return;

        if (endLevelGa.Score == _targetScore)
        {
            _winScreen.SetScore(endLevelGa.Score);
            _winScreen.SetReward(endLevelGa.Score * 10);
            ActionSystem.Instance.AddReaction(new OpenScreenGA(_winScreen));
        }
        else
        {
            _loseScreen.SetScore(endLevelGa.Score);
            _loseScreen.SetReward(endLevelGa.Score * 10);
            ActionSystem.Instance.AddReaction(new OpenScreenGA(_loseScreen));
        }
    }

    private IEnumerator LevelTimerRoutine()
    {
        while (_currentTime > 0 && _isRunning && _currentScore != _targetScore)
        {
            SpawnBalloon();
            yield return new WaitForSeconds(1f);

            _currentTime--;
            UpdateTimerUI();
        }
        
        EndLevel();
    }

    private void EndLevel()
    {
        if (!_isRunning) return;

        _isRunning = false;

        if (_levelRoutine != null)
        {
            StopCoroutine(_levelRoutine);
            _levelRoutine = null;
        }

        _balloonPool.ClearAll();

        int reward = _currentScore * 10;

        int stars = LevelStarSystem.Instance.CalculateStars(_currentScore, _targetScore);

        SaveSystem.Instance.AddScoreAndReward(_currentScore, reward);
        SaveSystem.Instance.SaveLevelStars(_levelIndex, stars);

        ActionSystem.Instance.Perform(new EndLevelGA(_currentScore, stars, _levelIndex));
    }

    private void SpawnBalloon()
    {
        if (!_isRunning) return;

        var balloon = _balloonPool.GetBalloon();
        if (balloon == null) return;

        balloon.transform.SetParent(null);

        Vector3 spawnPos = _spawnAreaCenter.position + new Vector3(
            Random.Range(-_spawnAreaSize.x / 2f, _spawnAreaSize.x / 2f),
            -_spawnOffset,
            Random.Range(-_spawnAreaSize.z / 2f, _spawnAreaSize.z / 2f)
        );

        balloon.transform.position = spawnPos;

        Vector3 targetPos = new Vector3(spawnPos.x, _spawnAreaCenter.position.y + _spawnOffset, spawnPos.z);

        balloon.OnPopped += OnBalloonPopped;
        balloon.FlyTo3D(targetPos, _flyDuration);
    }

    private void OnBalloonPopped(Balloon balloon)
    {
        if (!_isRunning) return;

        balloon.OnPopped -= OnBalloonPopped;
        _currentScore++;
        UpdateScoreUI();
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
}