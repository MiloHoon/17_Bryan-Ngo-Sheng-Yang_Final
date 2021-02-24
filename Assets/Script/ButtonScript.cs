using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void QuitScene()
    {
        Application.Quit();
    }
}
