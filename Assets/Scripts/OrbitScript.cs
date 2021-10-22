using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class OrbitScript : MonoBehaviour
{
    #region Events
    public UnityEvent Launch;
    #endregion

    #region Fields
    private float _baseRotationSpeed;
    private float _baseLaunchSpeed;
    private float _rotationSpeed = 50f;
    private float _launchSpeed = 10f;

    private GameObject _parentMoon;
    private eStateOrbiter _state;
    private bool _canLaunch = false;
    private bool _autoLaunch = false;
    private GameObject _currentObstacle = null;
    private float _nextRotation = 0;
    private float _timePenalty = 1;
    private float _timerPenalty = 0;

    public GameObject StartingMoon;
    public eStateOrbiter State { get => _state; }
    public GameObject ParentMoon { get => _parentMoon; }
    public float LaunchSpeed { get => _launchSpeed; }
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        if (StartingMoon != null)
            _parentMoon = StartingMoon;
        else
            _parentMoon = GameObject.FindGameObjectWithTag("Moon");

        _state = eStateOrbiter.Launch;

        SettingsManager.TryGet("RotationSpeed", ref _rotationSpeed);
        SettingsManager.TryGet("LaunchSpeed", ref _launchSpeed);
        SettingsManager.TryGet("AutoLaunch", ref _autoLaunch);
        SettingsManager.TryGet("TimePenalty", ref _timePenalty);
        _baseRotationSpeed = _rotationSpeed;
        _baseLaunchSpeed = _launchSpeed;

        GameObject.FindGameObjectWithTag("GameController")
            .GetComponent<DifficultyManager>().DifficultyChanged.AddListener(DifficultyChangedHandler);
    }

    // Update is called once per frame
    void Update()
    {
        if (_timerPenalty > 0)
        {
            _timerPenalty -= Time.deltaTime;
        }
        if (_state == eStateOrbiter.Orbit)
        {
            if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || _autoLaunch == true) 
                && _canLaunch)
            {
                _state = eStateOrbiter.Launch;
                _parentMoon = GameObject.FindGameObjectsWithTag("Moon").Where(o => o.Equals(_parentMoon) == false).First();
                Launch.Invoke();
            }
            else
            {
                if (Input.anyKeyDown)
                {
                    if (_currentObstacle != null && _timerPenalty <= 0)
                    {
                        char text = _currentObstacle.GetComponent<ObstacleScript>().Text;
                        KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), text.ToString());
                        if (Input.GetKeyDown(key))
                        {
                            _currentObstacle.GetComponent<ObstacleScript>().Deactivate();
                        }
                    }
                    else if (_currentObstacle == null)
                    {
                        _timerPenalty = _timePenalty;
                    }
                }
                this.transform.RotateAround(_parentMoon.transform.position, Vector3.forward, _rotationSpeed * Time.deltaTime);
            }
        }
        if (_state == eStateOrbiter.Launch)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, _parentMoon.transform.position, _launchSpeed * Time.deltaTime);
            if (Vector3.Distance(this.transform.position, _parentMoon.transform.position) <= 3)
            {
                _state = eStateOrbiter.Orbit;
                _canLaunch = false;

                this.transform.rotation = Quaternion.Euler(0, 0, _nextRotation);
                _nextRotation = (_nextRotation + 180) % 360;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Elevator"))
        {
            _canLaunch = true;
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            _currentObstacle = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Elevator"))
        {
            _canLaunch = false;
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            _currentObstacle = null;
        }
    }

    private void DifficultyChangedHandler()
    {
        _rotationSpeed = _baseRotationSpeed * DifficultyManager.SpeedModifier;
        _launchSpeed = _baseLaunchSpeed * DifficultyManager.SpeedModifier;
    }
    #endregion Methods
}

public enum eStateOrbiter
{
    Orbit,
    Launch
}
