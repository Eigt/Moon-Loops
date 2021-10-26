using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerScript : MonoBehaviour
{
    #region Events
    public UnityEvent TimeOut;
    #endregion

    #region Fields
    private float _timeInSeconds = 60;
    private float _timer = -1;

    public Color lowTimeColor = Color.red;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        SettingsManager.TryGet("TimeInSeconds", ref _timeInSeconds);

        TimeSpan time = TimeSpan.FromSeconds(_timeInSeconds);
        GetComponent<TextMesh>().text = time.ToString(@"m\:ss");
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(_timer);
            if (_timer <= 0)
            {
                GetComponent<TextMesh>().text = "0:00";
                TimeOut?.Invoke();
            }
            else
            {
                if (_timer <= 10)
                {
                    GetComponent<TextMesh>().color = lowTimeColor;
                }
                GetComponent<TextMesh>().text = time.ToString(@"m\:ss");
            }
        }
    }

    public void FirstOrbitHandler()
    {
        _timer = _timeInSeconds;
    }
    #endregion
}
