using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerScript : MonoBehaviour
{
    #region Fields
    private float _trackSpeed = 5;
    private Vector3 _offset;
    private float _clampMinX = -10;
    private float _clampMaxX = 10;

    public OrbitScript TrackedOrbiter;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        SettingsManager.TryGet("CameraMinX", ref _clampMinX);
        SettingsManager.TryGet("CameraMaxX", ref _clampMaxX);
        if (_clampMinX == _clampMaxX)
        {
            Debug.Log("Min and Max Camera X are equal.");
        }

        if (SettingsManager.TryGet("TrackSpeed", ref _trackSpeed) == false)
        {
            _trackSpeed = TrackedOrbiter.LaunchSpeed;
        }

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

        // Clamp camera position to avoid centreing on moon
        Vector3 pos = transform.position;
        pos = new Vector3(Mathf.Clamp(pos.x, _clampMinX, _clampMaxX), pos.y, pos.z);
        transform.position = pos;
    }
    #endregion
}
