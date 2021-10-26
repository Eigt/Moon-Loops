using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Events
    public UnityEvent ScoreChanged;
    public UnityEvent DifficultyChanged;
    #endregion

    #region Fields
    private static GameManager _instance;

    private static int _score = 0;
    private static float _speedModifier = 1;

    private float _speedCurve = 0.2f;
    private int _scoreInterval = 10;

    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.Log("GameManager is null");
            }
            return _instance;
        }
    }

    public static int Score { get=>_score; }

    public static float SpeedModifier { get => _speedModifier; }

    #endregion

    #region Methods
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SettingsManager.TryGet("ScoreInterval", ref _scoreInterval);
        SettingsManager.TryGet("DifficultyCurve", ref _speedCurve); 
    }

    public void ScoredHandler_1()
    {
        _score++;
        ScoredHandler_End();
    }

    public void ScoredHandler_2()
    {
        _score += 2;
        ScoredHandler_End();
    }

    private void ScoredHandler_End()
    {
        _speedModifier = 1 + (int)(_score / _scoreInterval) * _speedCurve;
        ScoreChanged?.Invoke();
        DifficultyChanged?.Invoke();
    }
    #endregion
}
