using System.Linq;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public char Text = 'X';

    private float _timeToReset = 0;
    private float _timerReset;
    private float _timeToExpand = 1;
    private float _timerExpand;
    char[] _alphabet = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (char)i).ToArray();

    // Start is called before the first frame update
    void Start()
    {
        SettingsManager.TryGet("TimeToReset", ref _timeToReset);
        SettingsManager.TryGet("TimeToExpand", ref _timeToExpand);
        _timerExpand = _timeToExpand;

        Text = _alphabet[Random.Range(0, _alphabet.Length)];
        GetComponentInChildren<TextMesh>().text = Text.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timerReset > 0)
        {
            _timerReset -= Time.deltaTime;
            if (_timerReset <= 0)
            {
                Reset();
            }
        }
        if (_timerExpand < _timeToExpand)
        {
            _timerExpand += Time.deltaTime;
            float scaleValue = Mathf.Clamp((float)(_timerExpand / _timeToExpand), 0.1f, 1);
            Vector3 newScale = new Vector3(scaleValue, scaleValue, 1);
            GetComponentInChildren<TextMesh>().transform.localScale = newScale;
        }
    }

    public void Deactivate()
    {
        GetComponentInChildren<TextMesh>().gameObject.SetActive(false);
        Text = _alphabet[Random.Range(0, _alphabet.Length)];
        _timerReset = _timeToReset;
    }

    public void Reset()
    {
        GetComponentInChildren<TextMesh>(true).gameObject.SetActive(true);
        GetComponentInChildren<TextMesh>().transform.localScale = new Vector3(0.1f, 0.1f, 1);
        GetComponentInChildren<TextMesh>().text = Text.ToString();
        _timerExpand = 0;
    }
}
