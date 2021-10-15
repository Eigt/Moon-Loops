using System.Linq;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    #region Fields
    public char Text = 'X';
    public bool IsActive = true;

    private float _timeToExpand = 1;
    private float _timerExpand;
    #endregion Fields

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        SettingsManager.TryGet("TimeToExpand", ref _timeToExpand);
        _timerExpand = _timeToExpand;

        //Text = _alphabet[Random.Range(0, _alphabet.Length)];
        GetComponentInChildren<TextMesh>().text = Text.ToString();
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

    public void Activate()
    {
        if (IsActive == false)
        {
            GetComponentInChildren<TextMesh>(true).gameObject.SetActive(true);
            IsActive = true;
            GetComponentInChildren<TextMesh>().transform.localScale = new Vector3(0.1f, 0.1f, 1);
            GetComponentInChildren<TextMesh>().text = Text.ToString();
            _timerExpand = 0;
        }
    }

    public void Deactivate()
    {
        if (IsActive == true)
        {
            GetComponentInChildren<TextMesh>().gameObject.SetActive(false);
            IsActive = false;
            //Text = _alphabet[Random.Range(0, _alphabet.Length)];
            //_timerReset = _timeToReset;
        }
    }
    #endregion
}
