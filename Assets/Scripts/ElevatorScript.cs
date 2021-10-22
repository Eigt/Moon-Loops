using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    #region Fields
    private bool _canActivate = false;

    public Color DefaultColor = Color.white;
    public Color ActiveColor = Color.yellow;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        bool autoLaunch = false;
        SettingsManager.TryGet("AutoLaunch", ref autoLaunch);
        _canActivate = !autoLaunch;
        if (_canActivate == false)
        {
            GetComponent<CapsuleCollider2D>().size = new Vector2(0.16f, 0.16f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_canActivate)
        {
            GetComponent<SpriteRenderer>().color = ActiveColor;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_canActivate)
        {
            GetComponent<SpriteRenderer>().color = DefaultColor;
        }
    }
    #endregion
}
