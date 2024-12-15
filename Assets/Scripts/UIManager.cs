using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject levelCompleteScreen;
    [SerializeField] private GameObject levelSelectionScreen;

    private int sceneCount;

    public Sprite[] levelPreviews;
    private Image image;

    private int index = 1;

    
    void Start()
    {
        sceneCount = SceneManager.sceneCountInBuildSettings;
        if (gameOverScreen)
        {
            gameOverScreen.SetActive(false);
        }
        if (pauseScreen)
        {
            pauseScreen.SetActive(false);
        }
        if (levelCompleteScreen)
        {
            levelCompleteScreen.SetActive(false);
        }
        if (levelSelectionScreen)
        {
            levelSelectionScreen.SetActive(false);
            image = levelSelectionScreen.transform.GetChild(levelSelectionScreen.transform.childCount - 1).GetComponent<Image>();
        }
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
        ResetValues();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        ResetValues();
    }

    private void ResetValues()
    {
        Finish.isClosed = true;
        Score.score = 0;
        ItemsController.index = 0;
        Score.minutes = 0.0f;
        Score.seconds = 0.0f;
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

    #region LevelComplete
    public void LevelComplete()
    {
        Text timeValueText = levelCompleteScreen.transform.GetChild(2).gameObject.transform.GetChild(1).GetComponent<Text>();
        Text scoreValueText = levelCompleteScreen.transform.GetChild(2).gameObject.transform.GetChild(3).GetComponent<Text>();
        timeValueText.text = Score.parseTime();
        scoreValueText.text = Score.score.ToString();
        
        levelCompleteScreen.SetActive(true);
        Time.timeScale = 0;
    }
    #endregion

    #region LevelSelection()
    public void OpenLevelSelection()
    {
        levelSelectionScreen.SetActive(true);
        SceneManager.GetSceneAt(index);
    }

    public void CloseLeveSelection()
    {
        levelSelectionScreen.SetActive(false);
    }


    public void changeToNextLevel()
    {
        index++;
        if (index > sceneCount - 1)
        {
            index--;
            return;
        }
        else
        {
            image.sprite = levelPreviews[index - 1];
        }
    }

    public void changeToPreviousLevel()
    {
        index--;
        if (index == 0)
        {
            index++;
            return;
        }
        else
        {
            image.sprite = levelPreviews[index - 1];
        }
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene(index);
    }
    #endregion
}
