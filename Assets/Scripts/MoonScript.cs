using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoonScript : MonoBehaviour
{
    #region Events
    public UnityEvent Scored_1;
    public UnityEvent Scored_2;
    #endregion

    #region Fields
    private ObstacleScript[] ObstacleChildren { get; set; }

    private float _timeToExpand = 1;
    private float _timerExpand;
    private bool _changeWord = false;

    public string CurrentWord { get; private set; }
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        SettingsManager.TryGet("TimeToExpand", ref _timeToExpand);
        _timerExpand = _timeToExpand;

        ObstacleChildren = GetComponentsInChildren<ObstacleScript>();
        GetNewWord();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timerExpand < _timeToExpand)
        {
            _timerExpand += Time.deltaTime;
            float scaleValue = Mathf.Clamp((float)(_timerExpand / _timeToExpand), 0.1f, 1);
            Vector3 newScale = new Vector3(scaleValue, scaleValue, 1);
            GetComponentInChildren<TextMesh>().transform.localScale = newScale;
        }
    }

    private void GetNewWord()
    {
        int wordLength = ObstacleChildren.Length; // word length defined by number of letters orbiting moon
        CurrentWord = WordManager.GetRandomWord(wordLength);
        GetComponentInChildren<TextMesh>().text = CurrentWord;
        for (int i = 0; i < wordLength; i++)
        {
            ObstacleChildren[i].Text = CurrentWord[i];
            ObstacleChildren[i].Activate();
        }
    }

    public void CheckForScore()
    {
        bool wordComplete = true;
        foreach (ObstacleScript child in ObstacleChildren)
        {
            if (child.IsActive == true)
            {
                wordComplete = false;
                break;
            }
        }

        if (wordComplete)
        {
            _changeWord = true;
            Scored_2?.Invoke();
        }
        else
        {
            Scored_1?.Invoke();
        }
    }

    public void LaunchHandler()
    {
        if (_changeWord)
        {
            _changeWord = false;
            GetNewWord();
            _timerExpand = 0;
        }
        else
        {
            // activate any activated children
            foreach (ObstacleScript child in ObstacleChildren)
            {
                child.Activate();
            }
        }
    }
    #endregion
}
