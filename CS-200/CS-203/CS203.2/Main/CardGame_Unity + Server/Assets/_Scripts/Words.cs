using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#region using editor
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

public class Words : MonoBehaviour
{
    #region local variables
    [SerializeField] TextAsset WORD_LIST_RAW;
    private WordList words;
    #endregion

    // Awake is calle when the script is being loaded
    void Awake()
    {
        words = new WordList(WORD_LIST_RAW);
    }
    
    /// <summary>
    /// check if the word list contains a word
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    public bool isWord(string word)
    {
        //Debug.Log(word.Length);
        //Debug.Log(words.Contains(word));
        return words.Contains(word);
    }

    /// <summary>
    /// a word list with custom functionality
    /// </summary>
    class WordList
    {
        public readonly string[] WordArray; // not to be changed

        /// <summary>
        /// ctor for word list
        /// </summary>
        /// <param name="wl">text asset</param>
        public WordList(TextAsset wl)
        {
            if (wl == null)
            {
                Debug.LogError("NO WORD LIST FOUND...");
                return;
            }
            WordArray = wl.text.Split("\n");
        }

        /// <summary>
        /// check if a list contains a word<br/>
        /// mac and windows tested working
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool Contains(string word)
        {
            word = word.ToUpper();
#if UNITY_STANDALONE_OSX
            return WordArray.Contains(word);
#else
            word += ((char)13).ToString();  // i have no fucking idea but it works
            return WordArray.Contains(word);
#endif

        }
    }

    /// <summary>
    /// converts a board to a list of words
    /// </summary>
    /// <param name="raw"></param>
    /// <returns></returns>
    public List<string> ConvertCharBoardToWords(List<List<char>> raw)
    {
        return ExtractStrings(raw);
    }

    /// <summary>
    /// extracts all strings from an array or board of letters
    /// </summary>
    /// <param name="lettersGrid"></param>
    /// <returns></returns>
    List<string> ExtractStrings(List<List<char>> lettersGrid)
    {
        int numRows = lettersGrid.Count;
        int numCols = lettersGrid[0].Count;
        List<string> returnList = new List<string>();

        for (int row = 0; row < numRows; row++)
        {
            string horizontalString = "";
            for (int col = 0; col < numRows; col++)
            {
                horizontalString += lettersGrid[row][col];
            }

            List<string> indivWords = horizontalString.Split(' ').ToList();
            int coll = 0;
            foreach (string word in indivWords)
            {
                if (word != " " && word != null && word != "")
                {
                    if (word.Length < 2)
                    {
                        if (CheckSingle(lettersGrid, word, row, coll))
                        {
                            if (!returnList.Contains(word))
                            {
                                returnList.Add(word);
                            }
                        }
                    }
                    else { returnList.Add(word); }
                }
                coll += word.Length;
                if(word.Length == 0) { coll++; }
            }
        }

        /*
        foreach (List<char> row in lettersGrid)
        {
            // split into list with positions still accurate - comes with white space
            string horizontalString = new string(row.ToArray());
            List<string> indivWords = horizontalString.Split(' ').ToList(); 
            foreach(string word in indivWords)
            {
                if(word != " " && word != null && word != "")
                {
                    returnList.Add(word);
                }

            }
        }*/

        

        for (int col = 0; col < numCols; col++)
        {
            string verticalString = "";
            for (int row = 0; row < numRows; row++)
            {
                verticalString += lettersGrid[row][col];
            }

            int roww = 0;
            List<string> indivWords = verticalString.Split(' ').ToList();
            foreach (string word in indivWords)
            {
                if (word != " " && word != null && word != "")
                {
                    if (word.Length < 2) 
                    { 
                        if(CheckSingle(lettersGrid, word, roww, col))
                        {
                            if (!returnList.Contains(word))
                            {
                                returnList.Add(word);
                            }
                        } 
                    }
                    else 
                    { 
                        returnList.Add(word); 
                    }
                }
                roww += word.Length;
                if (word.Length == 0) { roww++; }
            }
        }

        return returnList;
    }
    
    /// <summary>
    /// checks for single letters in both x and y direction
    /// </summary>
    /// <param sendName="raw"></param>
    /// <param sendName="word"></param>
    /// <param sendName="row"></param>
    /// <param sendName="col"></param>
    /// <returns>true if single letter else false if part of a word</returns>
    bool CheckSingle(List<List<char>> raw, string word, int row, int col)
    {
        int numRows = raw.Count;
        int numCols = raw[0].Count;
        bool isSingle = true;

        // Check above
        if (row > 0 && raw[row - 1][col] != ' ')
        {
            char c = raw[row - 1][col];
            if(c != ' ') { isSingle = false; }
        }

        // Check below
        if (row < numRows - 1 && raw[row + 1][col] != ' ')
        {
            char c = raw[row + 1][col];
            if (c != ' ') { isSingle = false; }
        }

        // Check left
        if (col > 0 && raw[row][col - 1] != ' ')
        {
            char c = raw[row][col - 1];
            if (c != ' ') { isSingle = false; }
        }

        // Check right
        if (col < numCols - 1 && raw[row][col + 1] != ' ')
        {
            char c = raw[row][col + 1];
            if (c != ' ') { isSingle = false; }
        }

        return isSingle;
    }
}


// *******************************************
// custom editor below - wont be in any builds
// *******************************************

#if UNITY_EDITOR
[CustomEditor(typeof(Words))]
public class EDITOR_Words : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Test"))
        {
            Words words = (Words)target;
            words.ConvertCharBoardToWords(words.GetComponent<Game>().lettersGrid);
        }
    }
}
#endif

