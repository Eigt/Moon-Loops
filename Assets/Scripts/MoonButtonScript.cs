using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Highlight(bool doHighlight)
    {
        if (doHighlight)
        {
            GetComponentInChildren<TextMesh>().fontStyle = FontStyle.Bold;
        }
        else
        {
            GetComponentInChildren<TextMesh>().fontStyle = FontStyle.Normal;
        }
    }
}
