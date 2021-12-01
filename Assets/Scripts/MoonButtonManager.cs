using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoonButtonManager : MonoBehaviour
{
    private MoonButtonScript[] MoonButtons { get; set; }
    private int _index;

    // Start is called before the first frame update
    void Start()
    {
        MoonButtons = GetComponentsInChildren<MoonButtonScript>();
        if (MoonButtons != null && MoonButtons.Length > 0)
        {
            MoonButtons[0].GetComponent<MoonButtonScript>().Highlight(true);
            _index = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MoonButtons != null && MoonButtons.Length > 0)
        {
            // navigate selection right
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoonButtons[_index].Highlight(false);
                _index++;
                if (_index >= MoonButtons.Length) // overflow
                {
                    _index = 0;
                }
                MoonButtons[_index].Highlight(true);
            }
            // navigate selection left
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoonButtons[_index].Highlight(false);
                _index--;
                if (_index < 0) // underflow
                {
                    _index = MoonButtons.Length - 1;
                }
                MoonButtons[_index].Highlight(true);
            }
            // enter selected level
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                SelectLevel();
            }
        }
    }

    void SelectLevel()
    {
        switch (_index)
        {
            case 0:
                SceneManager.LoadScene("Words3Scene");
                break;
            case 1:
                SceneManager.LoadScene("Words4Scene");
                break;
            default:
                throw new UnityException("Unhandled level selected");
        }
    }
}
