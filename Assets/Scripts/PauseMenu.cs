using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }

    public void RestartGame()
    {
        GameIsPaused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
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


    public void QuitGame()
    {
        Application.Quit();
    }
}
