using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{

    public GameObject HighScoresInputField;
    public GameObject HighScoresButton;
    public GameObject NextLevelButton;
    public GameObject winningText;
    public InputField nameInputField;

    void Start()
    {
        int CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        if (CurrentLevel == 2) {
            HighScoresInputField.SetActive(true);
            HighScoresButton.SetActive(true);
            winningText.SetActive(true);
            NextLevelButton.SetActive(false);
        }
    }

    public void PlayLevelAgain()
    {
        PlayerPrefs.SetInt("CurrentGold", 0);
        print("PlayLevelAgain PlayerPrefsGold" + PlayerPrefs.GetInt("CurrentGold"));
        SceneManager.LoadScene("Lvl1");

    }

    public void LoadNextLevel()
    {
        int lastLevel = PlayerPrefs.GetInt("CurrentLevel");
        lastLevel++;
        SceneManager.LoadScene(lastLevel);
    }

    public void BeginGame() {
        SceneManager.LoadScene("Lvl1");
    }

    public void LoadHighScores() {

        for (int i = 0; i < 5; i++) {
            int tempHighScore = PlayerPrefs.GetInt("HS" + i.ToString());
            int tempHighScore2 = 0;
            string tempHighScoreName = PlayerPrefs.GetString("HSP" + i.ToString());
            string tempHighScoreName2 = "";
            int currentGold = PlayerPrefs.GetInt("CurrentGold");
            if (currentGold > tempHighScore) {
                PlayerPrefs.SetInt("HS" + i.ToString(), currentGold);
                PlayerPrefs.SetString("HSP" + i.ToString(), nameInputField.text);
                for (int j = i + 1; j < 5; j++) {
                    tempHighScore2 = PlayerPrefs.GetInt("HS" + j.ToString());
                    tempHighScoreName2 = PlayerPrefs.GetString("HSP" + j.ToString());
                    PlayerPrefs.SetInt("HS" + j.ToString(), tempHighScore);
                    PlayerPrefs.SetString("HSP" + j.ToString(), tempHighScoreName);
                    tempHighScore = tempHighScore2;
                    tempHighScoreName = tempHighScoreName2;
                }

                break;
                SceneManager.LoadScene("HighScores");
            }
        }
        SceneManager.LoadScene("HighScores");

    }
}
