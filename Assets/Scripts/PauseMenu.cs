using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public bool isPaused = false;
    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;

    }

    public void Home()
    {
        isPaused = false;
        pauseMenu.SetActive(false);

        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;

    }

    public void ResumeGame()
    {
        isPaused = false;

        pauseMenu.SetActive(false);
        Time.timeScale = 1;

    }

    public void RestartGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;

    }
}
