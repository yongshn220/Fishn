using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestResol : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Screen.SetResolution(800, 800, false);
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Screen.SetResolution(400,800,false);
        }
    }
}
