using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private GameObject pauseScreen;

    
    private void Start()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    #region GameOver
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Score.score = 0;
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    #endregion

    #region Pause
    public void Pause()
    {
        if (pauseScreen.activeInHierarchy)
        {
            PauseGame(false);
        } 
        else
        {
            PauseGame(true);
        }
    }

    private void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);

        if (status)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    #endregion

    #region Level
    public void PlayLevel()
    {
        SceneManager.LoadScene(1);
    }
    #endregion
}
