using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrbitScript : MonoBehaviour
{
    public float RotationSpeed = 50f;
    public float LaunchSpeed = 10f;
    public GameObject StartingMoon;

    private GameObject _parentMoon;
    private eState _state;
    private bool _canLaunch = false;
    private GameObject _currentObstacle = null;
    private float _nextRotation = 180;

    // Start is called before the first frame update
    void Start()
    {
        if (StartingMoon != null)
            _parentMoon = StartingMoon;
        else
            _parentMoon = GameObject.FindGameObjectWithTag("Moon");

        if (SettingsManager.Settings.TryGetValue("RotationSpeed", out string rotSpd))
        {
            RotationSpeed = float.Parse(rotSpd);
        }
        if (SettingsManager.Settings.TryGetValue("LaunchSpeed", out string lncSpd))
        {
            LaunchSpeed = float.Parse(lncSpd);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_state == eState.Orbit)
        {
            if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && _canLaunch)
            {
                _state = eState.Launch;
                _parentMoon = GameObject.FindGameObjectsWithTag("Moon").Where(o => o.Equals(_parentMoon) == false).First();
            }
            else
            {
                if (Input.anyKeyDown && _currentObstacle != null)
                {
                    char text = _currentObstacle.GetComponent<ObstacleScript>().Text;
                    KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), text.ToString());
                    if (Input.GetKeyDown(key))
                    {
                        _currentObstacle.GetComponent<ObstacleScript>().Deactivate();
                    }
                }
                this.transform.RotateAround(_parentMoon.transform.position, Vector3.forward, RotationSpeed * Time.deltaTime);
            }
        }
        if (_state == eState.Launch)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, _parentMoon.transform.position, LaunchSpeed * Time.deltaTime);
            if (Vector3.Distance(this.transform.position, _parentMoon.transform.position) <= 3)
            {
                _state = eState.Orbit;
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

    internal enum eState
    {
        Orbit,
        Launch
    }
}
