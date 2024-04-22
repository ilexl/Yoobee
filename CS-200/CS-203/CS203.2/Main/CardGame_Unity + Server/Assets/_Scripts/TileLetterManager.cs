using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TileLetterManager : MonoBehaviour, IDropHandler
{
    #region local variables
    [SerializeField] Transform lettersParent;
    [SerializeField] GameObject prefabLetter;
    [SerializeField] Transform playerLettersBottom;
    [SerializeField] List<int> remainingLettersBag;
    [SerializeField] List<int> defaultLettersBag;
    [SerializeField] Transform letterStartRect;
    [SerializeField] Canvas canvas;
    [SerializeField] Vector2 StartPosOffset;
    [SerializeField] Transform boardParent;
    #endregion

    /// <summary>
    /// sets the letter bag to default for a new game
    /// </summary>
    public void ResetLettersBag()
    {
        remainingLettersBag = defaultLettersBag;
    } 

    /// <summary>
    /// gets all the letters in a list
    /// </summary>
    /// <returns></returns>
    public List<TileLetter> GetAllLetters()
    {
        List<TileLetter> allLetters = new List<TileLetter>();
        foreach (Transform t in lettersParent)
        {
            allLetters.Add(t.GetComponent<TileLetter>());
        }
        return allLetters;
    }

    /// <summary>
    /// not used but required for drag drop functionality
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData) { }

    /// <summary>
    /// counts the current playable letters
    /// </summary>
    /// <returns></returns>
    public int PlayersCurrentLetters()
    {
        int count = 0;
        foreach(Transform t in lettersParent)
        {
            TileLetter tileLetter;
            if(t.TryGetComponent(out tileLetter))
            {
                if (tileLetter.GetComponent<DragDrop>().playable)
                {
                    count++;
                }
            }
        }
        return count;
    }

    /// <summary>
    /// spawns in letters for player based on max
    /// </summary>
    /// <param name="most">max a player can have right now</param>
    public void SpawnPlayerLetters(int most)
    {
        int currentAmount = PlayersCurrentLetters();
        while(currentAmount < most)
        {
            SpawnLetter();
            currentAmount = PlayersCurrentLetters();
        }
    }

    /// <summary>
    /// spawns a letter a player can move
    /// </summary>
    private void SpawnLetter()
    {
        GameObject newLetter = Instantiate(prefabLetter, lettersParent);
        char c = RandomLetter();
        //Debug.Log((int)c);
        newLetter.GetComponentInChildren<TextMeshProUGUI>().text = c.ToString();
        newLetter.transform.position = new Vector3(Random.Range(-10000, 10000), Random.Range(-10000, 10000), 0);

        newLetter.GetComponent<DragDrop>().actualPosition = RandomPosition(newLetter.transform);
        newLetter.GetComponent<TileLetter>().SetPlayable(true);
    }

    /// <summary>
    /// picks a ranom pos
    /// </summary>
    /// <param name="newLetter"></param>
    /// <returns></returns>
    public Vector2 RandomPosition(Transform newLetter)
    {
        RectTransform rectTransform = letterStartRect.GetComponent<RectTransform>();
        newLetter.SetParent(letterStartRect, false);

        Vector2 newPos = StartPosOffset;
        newPos += new Vector2(Random.Range(-(rectTransform.rect.width / 2.2f) , (rectTransform.rect.width / 2.2f)),Random.Range(-(rectTransform.rect.height / 4), 0));
        //Debug.Log(newPos);
        newLetter.SetParent(lettersParent, false);
        return newPos;
    }

    /// <summary>
    /// counts the letters left in the bag
    /// </summary>
    /// <returns></returns>
    public int RemainingLetters()
    {
        int remainingLetters = 0;
        foreach (int test in remainingLettersBag)
        {
            remainingLetters += test;
        }

        return remainingLetters;
    }

    /// <summary>
    /// picks a random letter from within the bag
    /// </summary>
    /// <returns></returns>
    private char RandomLetter()
    {
        int remaining = RemainingLetters();
        char c = ' ';
        if(remaining <= 0)
        {
            return c;
        }

        while (c == ' ')
        {
            int rand = Random.Range(0, 26);
            if(remainingLettersBag[rand] > 0)
            {
                c = (char)(65 + rand);
                remainingLettersBag[rand] -= 1;
            }
        }
        return c;
    }

    /// <summary>
    /// checks if a holder has a letter
    /// </summary>
    /// <param name="dh"></param>
    /// <returns></returns>
    public bool DropHolderHasLetter(DropHolder dh)
    {
        foreach(Transform letter in lettersParent)
        {
            TileLetter t;
            if(letter.TryGetComponent(out t))
            {
                if(t.GetDragDrop().dropHolder == dh)
                {
                    return true;
                }
            }

        }
        return false;
    }

    /// <summary>
    /// sets all letters raycast block off so letter can drop on other letters without issue
    /// </summary>
    /// <param name="set"></param>
    public void RayCastSetAllLetters(bool set)
    {
        foreach (Transform letter in lettersParent)
        {
            TileLetter t;
            if (letter.TryGetComponent(out t))
            {
                t.GetDragDrop().RayCastSet(set);
            }

        }
    }

    /// <summary>
    /// bags players current letters
    /// </summary>
    public void BagPlayersLetters() 
    {
        foreach (Transform letter in lettersParent)
        {
            TileLetter t;
            if (letter.TryGetComponent(out t))
            {
                if (t.GetPlayable())
                {
                    int letterIndex = -65 + t.currentLetter;
                    remainingLettersBag[letterIndex]++;
                    Destroy(letter.gameObject);
                }
            }

        }
    }

    /// <summary>
    /// retrieves and moves all playable letters to specified area
    /// </summary>
    public void Retrieve()
    {
        for (int i = PlayersCurrentLetters() + 1; i > 0; i--) // no idea why this makes it work but its a easy and dirty fix
        {
            foreach (Transform letter in lettersParent)
            {
            
                TileLetter t;
                if (letter.TryGetComponent(out t))
                {
                    if (t.GetPlayable())
                    {
                        DragDrop dd = t.GetDragDrop();
                        dd.OnBeginDrag(null);
                        dd.OnEndDrag(null);
                        if (dd.dropHolder != null)
                        {
                            t.ResetDropHolder();
                        }
                        dd.actualPosition = RandomPosition(t.transform);
                    }
                }
            }
        }
    }

    /// <summary>
    /// check if a player has a specified letter
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public bool PlayerHasLetter(char c)
    {
        foreach (Transform letter in lettersParent)
        {
            TileLetter t;
            if (letter.TryGetComponent(out t))
            {
                if (t.GetPlayable())
                {
                    if(t.currentLetter == c)
                    {
                        return true;
                    }
                }
            }

        }
        return false;
    }

    /// <summary>
    /// picks the best work a player can make <br/>
    /// [NOT IMPLEMENTED YET]
    /// </summary>
    /// <returns></returns>
    public string BestPossiblePlay()
    {
        Debug.LogWarning("Best Possible Play NOT implemented yet...");
        return "NOT IMPLEMENTED YET";
    }

    /// <summary>
    /// removes a specified letter from player
    /// </summary>
    /// <param name="c"></param>
    public void RemovePlayerLetter(char c)
    {
        foreach (Transform letter in lettersParent)
        {
            TileLetter t;
            if (letter.TryGetComponent(out t))
            {
                if (t.GetPlayable())
                {
                    if (t.currentLetter == c)
                    {
                        Destroy(letter.gameObject);
                    }
                }
            }

        }
    }

    /// <summary>
    /// destroys every letter
    /// </summary>
    public void DestroyAllLetters()
    {
        foreach (Transform letter in lettersParent)
        {
            TileLetter t;
            if (letter.TryGetComponent(out t))
            {
                Destroy(letter.gameObject);
            }
        }
    }

    /// <summary>
    /// spawns opponents letters on board when they play
    /// </summary>
    /// <param name="c"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SpawnOppLetterOnBoard(char c, int x, int y)
    {
        GameObject newLetter = Instantiate(prefabLetter, lettersParent);
        newLetter.GetComponentInChildren<TextMeshProUGUI>().text = c.ToString();
        newLetter.transform.position = new Vector3(Random.Range(-10000, 10000), Random.Range(-10000, 10000), 0);
        newLetter.GetComponent<DragDrop>().actualPosition = RandomPosition(newLetter.transform);
        newLetter.GetComponent<DragDrop>().Drop(FindDropHolder(x,y));
        newLetter.GetComponent<TileLetter>().SetPlayable(false);
    }

    /// <summary>
    /// finds a drop holder using coords
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private DropHolder FindDropHolder(int x, int y)
    {
        string contains = "(" + x + ".00, " + y + ".00, 0.00)";
        foreach(Transform boardp in boardParent)
        {
            if (boardp.name.Contains(contains))
            {
                return boardp.GetComponent<DropHolder>();
            }
        }
        return null;
    }
}
