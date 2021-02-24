using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool GameisPaused = false;

    public GameObject pausemenuUI;

    // Update is called once per frame
    void Update()
    {
        // When press esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPaused)
            {
                // Resume game
                Cursor.lockState = CursorLockMode.Locked;
                Resume();
            }
            else
            {
                // Pause game
                Cursor.lockState = CursorLockMode.None;
                Pause();
            }
        }
    }

    public void Resume()
    {
        // Resume and set the time to default
        pausemenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
    }

    void Pause()
    {
        // Pause and set time to 0
        pausemenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
    }

    public void LoadMenu()
    {
        // Change scene to menu scene
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }
    public void QuitGame()
    {
        // Quit game
        Application.Quit();
    }
}