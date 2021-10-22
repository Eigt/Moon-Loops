using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreScript : MonoBehaviour
{
    #region Events
    public UnityEvent ScoreChanged;
    #endregion

    #region Fields
    private int _score = 0;

    public int Score { get => _score; }
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject moon in GameObject.FindGameObjectsWithTag("Moon"))
        {
            moon.GetComponent<MoonScript>().Scored.AddListener(ScoredHandler);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMesh>().text = _score.ToString();
    }

    private void ScoredHandler()
    {
        _score++;
        ScoreChanged.Invoke();
    }
    #endregion
}
