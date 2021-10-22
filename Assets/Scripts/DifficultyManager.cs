using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DifficultyManager : MonoBehaviour
{
    #region Events
    public UnityEvent DifficultyChanged;
    #endregion

    #region Fields
    private static float _speedModifier = 1;
    private float _speedCurve = 0.2f;
    private int _scoreInterval = 10;
    private ScoreScript _scoreScript;

    public static float SpeedModifier { get => _speedModifier; }
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        _scoreScript = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreScript>();
        _scoreScript.ScoreChanged.AddListener(ScoreChangedHandler);

        SettingsManager.TryGet("ScoreInterval", ref _scoreInterval);
        SettingsManager.TryGet("DifficultyCurve", ref _speedCurve);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreChangedHandler()
    {
        _speedModifier = 1 + (int)(_scoreScript.Score / _scoreInterval) * _speedCurve; 
        DifficultyChanged.Invoke();
    }
    #endregion
}
