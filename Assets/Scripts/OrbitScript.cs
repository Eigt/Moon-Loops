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

    // Start is called before the first frame update
    void Start()
    {
        if (StartingMoon != null)
            _parentMoon = StartingMoon;
        else
            _parentMoon = GameObject.FindGameObjectWithTag("Moon");
    }

    // Update is called once per frame
    void Update()
    {
        if (_state == eState.Orbit)
        {
            if (Input.GetMouseButtonDown(0) && _canLaunch)
            {
                _state = eState.Launch;
                _parentMoon = GameObject.FindGameObjectsWithTag("Moon").Where(o => o.Equals(_parentMoon) == false).First();
            }
            else
            {

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
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _canLaunch = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _canLaunch = false;
    }

    internal enum eState
    {
        Orbit,
        Launch
    }
}
