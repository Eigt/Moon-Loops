using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour
{
    #region Fields
    public string CurrentWord { get; private set; }

    private ObstacleScript[] ObstacleChildren { get; set; }

    private float _timeToExpand = 1;
    private float _timerExpand;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        SettingsManager.TryGet("TimeToExpand", ref _timeToExpand);
        _timerExpand = _timeToExpand;

        ObstacleChildren = GetComponentsInChildren<ObstacleScript>();
        GetNewWord(3);
        GameObject.FindGameObjectWithTag("Player").GetComponent<OrbitScript>().Launch.AddListener(LaunchEvent);
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

    private void GetNewWord(int wordLength)
    {
        CurrentWord = WordManager.GetRandomWord(wordLength);
        GetComponentInChildren<TextMesh>().text = CurrentWord;
        for (int i = 0; i < ObstacleChildren.Length && i < CurrentWord.Length; i++)
        {
            ObstacleChildren[i].Text = CurrentWord[i];
            ObstacleChildren[i].Activate();
        }
    }

    private void LaunchEvent()
    {
        bool changeWord = true;
        foreach (ObstacleScript child in ObstacleChildren)
        {
            if (child.IsActive == true)
            {
                changeWord = false;
                break;
            }
        }

        if (changeWord)
        {
            GetNewWord(3);
            _timerExpand = 0;
        }
        else
        {
            foreach (ObstacleScript child in ObstacleChildren)
            {
                child.Activate();
            }
        }
    }
    #endregion
}
