using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResolutionSet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResolutionChange(int type)
    {
        if (type == 1)
        {
            Screen.SetResolution (800,600,false);
        }
        if (type == 2)
        {
            Screen.SetResolution (1024,768,false);
        }
        if (type == 3)
        {
            Screen.SetResolution (1280,768,false);
        }
        if (type == 4)
        {
            Screen.SetResolution (1920,1080,false);
        }
    }

        public void Return()
    {
        SceneManager.LoadScene(0);
    }

    public void gotoTheScene(string temp)
    {
        SceneManager.LoadScene(temp);
    }
}