using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene(1);    //load to which screen
    }

    public void Option()
    {
        SceneManager.LoadScene(2);
    }
    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
    }
}
