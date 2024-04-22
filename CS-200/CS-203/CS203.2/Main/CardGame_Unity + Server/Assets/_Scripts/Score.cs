using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    #region local variables
    [SerializeField] TextMeshProUGUI ownScoreGUI;
    [SerializeField] TextMeshProUGUI oppScoreGUI;
    [SerializeField] private int ownScore;
    [SerializeField] private int oppScore;
    #endregion

    // hard coded a-z values but can be changed if needed
    static readonly List<int> lettersScore = new List<int>()
    {
        1, 3, 3, 2, 1, 4, 2, 4, 1, 8, 5, 1, 3, 1, 1, 3, 10, 1, 1, 1, 1, 4, 4, 8, 4, 10
    };

    // Update is called once per frame
    private void Update()
    {
        // sets text
        ownScoreGUI.text = ownScore + " : YOU";
        oppScoreGUI.text = "OPP : " + oppScore;
    }

    /// <summary>
    /// gets clients score
    /// </summary>
    /// <returns></returns>
    public int GetOwnScore() { return ownScore; }

    /// <summary>
    /// gets opponents score
    /// </summary>
    /// <returns></returns>
    public int GetOppScore() { return oppScore; }

    /// <summary>
    /// resets score
    /// </summary>
    public void Reset() { ownScore = 0; oppScore = 0; }

    /// <summary>
    /// changes clients score
    /// </summary>
    /// <param name="change"></param>
    public void ChangeOwnScore(int change) { ownScore += change; }

    /// <summary>
    /// changes opponents score
    /// </summary>
    /// <param name="change"></param>
    public void ChangeOppScore(int change) { oppScore += change; }

    /// <summary>
    /// calculates the total score of a word
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    public static int CalculateBaseScore(string word)
    {
        int score = 0;
        foreach(char c in word)
        {
            score += lettersScore[-65 + c];
        }
        return score;
    }
}
