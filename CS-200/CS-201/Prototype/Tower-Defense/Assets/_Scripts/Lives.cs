using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    [SerializeField] int maxLives;
    [SerializeField] int currentLives;
    [SerializeField] Score score;
    [SerializeField] PlaySelect scenes;
    [SerializeField] TMPro.TextMeshProUGUI text;
    private void Awake() { currentLives = maxLives; }
    public int GetCurrentLives() { return currentLives; }
    public void ChangeLives(int change) { currentLives += change; }
    void Update()
    {
        text.text = "Lives : " + currentLives + "/" + maxLives;
        if (currentLives > 0) { return; }
        else
        {
            score.SaveCurrentScore();
            scenes.HighScore();
        }
    }
}
