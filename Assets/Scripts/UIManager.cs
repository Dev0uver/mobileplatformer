using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    PlatformerApi platformerApi = new PlatformerApi();
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject levelCompleteScreen;
    [SerializeField] private GameObject levelSelectionScreen;
    [SerializeField] private GameObject recordScreen;

    private int sceneCount;

    public Sprite[] levelPreviews;
    private Image image;
    private Text levelNumber;
    private Text levelNumberScore;

    private List<Text> time = new List<Text>();
    private List<Text> score = new List<Text>();
    private List<Text> nickname = new List<Text>();
    
    private List<Text> place = new List<Text>();

    private static int index = 1;
    private int record = 1;

    async void Start()
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
            levelNumber = levelSelectionScreen.transform.GetChild(levelSelectionScreen.transform.childCount - 2).GetComponent<Text>();
        }
        if (recordScreen)
        {

            recordScreen.SetActive(false);
            levelNumberScore = recordScreen.transform.GetChild(recordScreen.transform.childCount - 1).GetComponent<Text>();
            GameObject recordsObject = recordScreen.transform.GetChild(recordScreen.transform.childCount - 2).gameObject;

            GameObject placeObject = recordsObject.transform.GetChild(recordsObject.transform.childCount - 4).gameObject;
            for (int i = 1; i <= placeObject.transform.childCount; i++) {
                place.Add(placeObject.transform.GetChild(placeObject.transform.childCount - i).GetComponent<Text>());
            }
            GameObject timeObject = recordsObject.transform.GetChild(recordsObject.transform.childCount - 3).gameObject;
            for (int i = 1; i <= timeObject.transform.childCount; i++) {
                time.Add(timeObject.transform.GetChild(timeObject.transform.childCount - i).GetComponent<Text>());
            }
            GameObject scoreObject = recordsObject.transform.GetChild(recordsObject.transform.childCount - 2).gameObject;
            for (int i = 1; i <= scoreObject.transform.childCount; i++) {
                score.Add(scoreObject.transform.GetChild(scoreObject.transform.childCount - i).GetComponent<Text>());
            }
            GameObject nicknameObject = recordsObject.transform.GetChild(recordsObject.transform.childCount - 1).gameObject;
            for (int i = 1; i <= nicknameObject.transform.childCount; i++) {
                nickname.Add(nicknameObject.transform.GetChild(nicknameObject.transform.childCount - i).GetComponent<Text>());
            }
            RecordStruct[] rec = await platformerApi.GetRecordsAsync(record);

            time.Reverse();
            score.Reverse();
            nickname.Reverse();
            place.Reverse();

            for (int i = 0; i < timeObject.transform.childCount; i++) {
                if (i < rec.Length) {
                    time[i].text = rec[i].time.Substring(3);
                    score[i].text = rec[i].score.ToString();
                    nickname[i].text = rec[i].nickname;
                    place[i].text = $"{i + 1}.";
                } else {
                    time[i].text = "";
                    score[i].text = "";
                    nickname[i].text = "";
                    place[i].text = "";
                }
            }
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
    public async void LevelComplete()
    {
        Text timeValueText = levelCompleteScreen.transform.GetChild(2).gameObject.transform.GetChild(1).GetComponent<Text>();
        Text scoreValueText = levelCompleteScreen.transform.GetChild(2).gameObject.transform.GetChild(3).GetComponent<Text>();
        timeValueText.text = Score.parseTime();
        scoreValueText.text = Score.score.ToString();

        levelCompleteScreen.SetActive(true);
        Time.timeScale = 0;
        
        string time = timeValueText.text.Length == 4 ? "0" + timeValueText.text : timeValueText.text;
        RecordStruct recordStruct = new RecordStruct();
        recordStruct.time = "00:" + time;
        recordStruct.score = int.Parse(scoreValueText.text);
        recordStruct.level = index;
        await platformerApi.SaveRecordAsync(recordStruct);
    }
    #endregion

    #region LevelSelection()
    public void OpenLevelSelection()
    {
        image.sprite = levelPreviews[index - 1];
        levelNumber.text = index.ToString();
        levelSelectionScreen.SetActive(true);
        SceneManager.GetSceneAt(index);
    }

    public void CloseLevelSelection()
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
            levelNumber.text = index.ToString();
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
            levelNumber.text = index.ToString();
        }
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene(index);
        ResetValues();
    }

    public void NextLevel()
    {
        index++;
        PlayLevel();
    }
    #endregion

    #region RecordPage
    public void OpenRecordSelection()
    {
        recordScreen.SetActive(true);
    }

    public void CloseRecordSelection()
    {
        recordScreen.SetActive(false);
    }

    public async void changeToNextRecord()
    {
        record++;
        if (record > sceneCount - 1)
        {
            record--;
            return;
        }
        else
        {
            RecordStruct[] rec = await platformerApi.GetRecordsAsync(record);

            for (int i = 0; i < 10; i++) {
                if (i < rec.Length) {
                    time[i].text = rec[i].time.Substring(3);
                    score[i].text = rec[i].score.ToString();
                    nickname[i].text = rec[i].nickname;
                    place[i].text = $"{i + 1}.";
                } else {
                    time[i].text = "";
                    score[i].text = "";
                    nickname[i].text = "";
                    place[i].text = "";
                }
            }
            levelNumberScore.text = record.ToString();
        }
    }

    public async void changeToPreviousRecord()
    {
        record--;
        if (record == 0)
        {
            record++;
            return;
        }
        else
        {
            RecordStruct[] rec = await platformerApi.GetRecordsAsync(record);

            for (int i = 0; i < 10; i++) {
                if (i < rec.Length) {
                    time[i].text = rec[i].time.Substring(3);
                    score[i].text = rec[i].score.ToString();
                    nickname[i].text = rec[i].nickname;
                    place[i].text = $"{i + 1}.";
                } else {
                    time[i].text = "";
                    score[i].text = "";
                    nickname[i].text = "";
                    place[i].text = "";
                }
            }
            levelNumberScore.text = record.ToString();
        }
    }
#endregion
}