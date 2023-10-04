using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySelect : MonoBehaviour
{
    [SerializeField] int endlessSceneIndex = 1;
    [SerializeField] int campaignSceneIndex = 1;
    [SerializeField] int multiplayerSceneIndex = 1;
    [SerializeField] int titleSceneIndex = 1;
    [SerializeField] int highScoreIndex = 1;
    public void Campaign()
    {
        SceneManager.LoadScene(campaignSceneIndex);
    }

    public void Endless()
    {
        SceneManager.LoadScene(endlessSceneIndex);
    }

    public void Multiplayer()
    {
        SceneManager.LoadScene(multiplayerSceneIndex);
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene(titleSceneIndex);
    }

    public void HighScore()
    {
        SceneManager.LoadScene(highScoreIndex);
    }
}
