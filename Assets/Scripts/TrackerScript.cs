using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerScript : MonoBehaviour
{
    #region Fields
    public OrbitScript TrackedOrbiter;

    private float _trackSpeed = 5;
    private Vector3 _offset;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        _trackSpeed = TrackedOrbiter.LaunchSpeed;

        _offset = new Vector3(0, 0, transform.position.z - TrackedOrbiter.transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        switch (TrackedOrbiter.State)
        {
            case eStateOrbiter.Orbit:
                transform.position = Vector3.MoveTowards(transform.position, TrackedOrbiter.ParentMoon.transform.position + _offset, 
                    Time.deltaTime * _trackSpeed);
                break;
            case eStateOrbiter.Launch:
                transform.position = Vector3.MoveTowards(transform.position, TrackedOrbiter.ParentMoon.transform.position + _offset, 
                    Time.deltaTime * _trackSpeed);
                break;
        }
    }
    #endregion
}
