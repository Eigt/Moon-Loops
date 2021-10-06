using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public char Text = 'X';

    private float _timeToReset = 0;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        if (SettingsManager.Settings.TryGetValue("TimeToReset", out string timeVal))
        {
            _timeToReset = int.Parse(timeVal);
        }

        GetComponentInChildren<TextMesh>().text = Text.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                Reset();
            }
        }
    }

    public void Deactivate()
    {
        GetComponentInChildren<TextMesh>().gameObject.SetActive(false);
        Text = 'Z';
        _timer = _timeToReset;
    }

    public void Reset()
    {
        GetComponentInChildren<TextMesh>(true).gameObject.SetActive(true);
        GetComponentInChildren<TextMesh>().text = Text.ToString();
    }
}
