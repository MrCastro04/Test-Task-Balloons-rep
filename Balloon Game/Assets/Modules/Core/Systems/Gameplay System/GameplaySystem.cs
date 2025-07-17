using System.Collections;
using Modules.Core.Game_Actions;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.UI.Screens.Base_Screen;
using Modules.Core.Utility.Singleton;
using UnityEngine;
using TMPro;

public class GameplaySystem : Singleton<GameplaySystem>
{
    [Header("WIN/LOSE Settings")]
    [SerializeField] private WinScreen _winScreen;
    [SerializeField] private LoseScreen _loseScreen;
    
    [Header("Level Settings")]
    [SerializeField] private BalloonPool _balloonPool;
    [SerializeField] private float _levelTimer = 30f;
    [SerializeField] private float _flyDuration = 3f;
    [SerializeField] private float _spawnOffset = 5f;
    [SerializeField] private int _targetScore = 15;

    [Header("Spawn Area (3D)")]
    [SerializeField] private Transform _spawnAreaCenter; 
    [SerializeField] private Vector3 _spawnAreaSize = new Vector3(10f, 0f, 10f);

    [Header("UI")]
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _scoreText;

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

        _balloonPool.PopulatePool(30);
        _balloonsLeft = 30;

        UpdateTimerUI();
        UpdateScoreUI();

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
        if (balloon == null) return;

        _balloonsLeft--;

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
        balloon.OnPopped -= OnBalloonPopped;
        _currentScore++;
        UpdateScoreUI();

        if (_currentScore >= _targetScore && _isRunning)
        {
            EndLevel();
        }
    }
    
    private void EndLevel()
    {
        if (!_isRunning) return;

        _isRunning = false;
        Debug.Log($"Level Finished! Final Score: {_currentScore}/{_targetScore}");

        if (_levelRoutine != null)
        {
            StopCoroutine(_levelRoutine);
            _levelRoutine = null;
        }

        _balloonPool.ClearAll();

        if (_currentScore == _targetScore)
        {
            _winScreen.SetScore(_currentScore);
            
            ActionSystem.Instance.Perform(new OpenScreenGA(_winScreen));
            
            Debug.Log("You WIN!");
        }
        else
        {
            _loseScreen.SetScore(_currentScore);
            
            ActionSystem.Instance.Perform(new OpenScreenGA(_loseScreen));
            
            Debug.Log("You LOSE!");
        }
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
