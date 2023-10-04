using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    [SerializeField] int amountMax;
    [SerializeField] Score score;
    [SerializeField] WindowManager windowManager;
    [SerializeField] List<int> highScores = new();
    [SerializeField] List<string> highScoresNames = new();

    [SerializeField] int newHighScoreIndex;
    [SerializeField] TMPro.TMP_InputField input;
    [SerializeField] GameObject newHighScoreMenu;
    [SerializeField] GameObject highScoreHolder;
    [SerializeField] GameObject highScorePrefab;
    [SerializeField] TMPro.TextMeshProUGUI[] yourScores; 

    void Awake()
    {
        Debug.Log("HERE");
        newHighScoreMenu.SetActive(false);
        score.LoadCurrentScore();
        int savedScore = score.GetScore();
        if (savedScore == -1)
        {
            // Show the high score list - no change
            windowManager.ShowWindow(0);
            UpdateHighScore();
        }
        else
        {
            windowManager.ShowWindow(1);
            UpdateHighScore();  
        }
        score.ResetScore();
        score.SaveCurrentScore(); // make sure the new entry is not re read
    }
    void UpdateHighScore()
    {
        YourScore();
        LoadHighScores();
        for(int i = 0; i < amountMax; i++)
        {
            if (score.GetScore() > highScores[i])
            {
                highScores.Insert(i, score.GetScore());
                highScoresNames.Insert(i, "TEMP");
                newHighScoreIndex = i;
                newHighScoreMenu.SetActive(true);
                break;
            }
        }
        DisplayHighScores();
        SaveHighScores();
    }
    void SaveHighScores()
    {
        for (int i = 0; i < amountMax; i++)
        {
            PlayerPrefs.SetInt("LEGNER-STUDIO-GAME*TD*" + "HIGHSCORES*" + i.ToString(), highScores[i]);
            PlayerPrefs.SetString("LEGNER-STUDIO-GAME*TD*" + "HIGHSCORESNAMES*" + i.ToString(), highScoresNames[i]);
        }
    }
    void LoadHighScores()
    {
        highScores = new();
        highScoresNames = new();

        for(int i = 0; i < amountMax; i++)
        {
            highScores.Add(PlayerPrefs.GetInt("LEGNER-STUDIO-GAME*TD*" + "HIGHSCORES*" + i.ToString(), 0));
            highScoresNames.Add(PlayerPrefs.GetString("LEGNER-STUDIO-GAME*TD*" + "HIGHSCORESNAMES*" + i.ToString(), "NONE"));
        }
    }
    public void AddNameNewHighScore()
    {
        highScoresNames[newHighScoreIndex] = input.text;
        SaveHighScores();
        newHighScoreMenu.SetActive(false);
        windowManager.ShowWindow(0);
        DisplayHighScores();
    }
    void DisplayHighScores()
    {
        foreach(Transform child in highScoreHolder.transform) { Destroy(child.gameObject); }
        for(int i = 0; i < amountMax; i++)
        {
            GameObject hs = Instantiate(highScorePrefab, highScoreHolder.transform);
            Transform pos = hs.transform.Find("Position");
            Transform name = hs.transform.Find("Name");
            Transform score = hs.transform.Find("Score");

            pos.GetComponent<TMPro.TextMeshProUGUI>().text = (i + 1).ToString() + ".";
            name.GetComponent<TMPro.TextMeshProUGUI>().text = highScoresNames[i];
            score.GetComponent<TMPro.TextMeshProUGUI>().text = "Score : " + highScores[i].ToString();
        }
    }

    void YourScore()
    {
        foreach(TMPro.TextMeshProUGUI s in yourScores)
        {
            s.text = "You scored : " + score.GetScore().ToString() + " points!";
        }
    }
}
