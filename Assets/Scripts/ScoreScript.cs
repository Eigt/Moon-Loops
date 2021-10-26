using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreScript : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TextMesh>().text = "0";
    }

    public void ScoredHandler()
    {
        GetComponent<TextMesh>().text = GameManager.Score.ToString();
    }
}
