using System.Linq;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public char Text = 'X';

    private float _timeToReset = 0;
    private float _timer;
    char[] _alphabet = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (char)i).ToArray();

    // Start is called before the first frame update
    void Start()
    {
        SettingsManager.TryGet("TimeToReset", ref _timeToReset);

        Text = _alphabet[Random.Range(0, _alphabet.Length)];
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
        Text = _alphabet[Random.Range(0, _alphabet.Length)];
        _timer = _timeToReset;
    }

    public void Reset()
    {
        GetComponentInChildren<TextMesh>(true).gameObject.SetActive(true);
        GetComponentInChildren<TextMesh>().text = Text.ToString();
    }
}
