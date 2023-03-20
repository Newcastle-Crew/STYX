#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#endregion

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    [SerializeField] GameObject gameplayUI;

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (GameIsPaused)
            { 
                Resume();
                gameplayUI.SetActive(true);
            }
            else
            {
                gameplayUI.SetActive(false);
                Pause();
            }
        }
    }

    public void Resume()
    {
        //Cursor.visible = false;
        Time.timeScale = 1f;
        GameIsPaused = false;
        //Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
    }

    public void Pause()
    {
        //Cursor.visible = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
        //Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
    }
}