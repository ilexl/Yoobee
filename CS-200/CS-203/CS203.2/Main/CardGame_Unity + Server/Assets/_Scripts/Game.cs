using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Riptide;
#region using editor
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

public class Game : MonoBehaviour
{
    #region public variables
    public bool isMultiplayer = false;
    public Board board;
    public string OtherPlayerUsername = "";
    public List<List<char>> lettersGrid;
    public PopUpManager popUpManager;
    public bool localTurn = false;
    #endregion
    #region local variables
    [SerializeField] TileLetterManager tileLetters;
    [SerializeField] Words words;
    [SerializeField] Score score;
    [SerializeField] int startingLettersAmount;
    [SerializeField] Button playButton;
    [SerializeField] WindowManager mainWindowManager;
    [SerializeField] Window mainMenuWindow;
    [SerializeField] NetworkManager networkManager;
    [SerializeField] Window waitforgame;
    List<string> allPreviousWords;
    int pointsMultiplier = 1;
    private static Game _singleton;
    #endregion

    /// <summary>
    /// ensures there is one of this object open at a time (destroys others)
    /// </summary>
    public static Game Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(Game)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }

    // Awake is called when the script is loaded
    void Awake()
    {
        Singleton = this;
        #region load checks
        if (board == null)
        {
            board = gameObject.GetComponentInChildren<Board>();
        }
        if (board == null)
        {
            Debug.LogError("Board not found...");
        }
        if (tileLetters == null)
        {
            tileLetters = gameObject.GetComponentInChildren<TileLetterManager>();
        }
        if (tileLetters == null)
        {
            Debug.LogError("TileLetters not found...");
        }
        if (popUpManager == null)
        {
            popUpManager = gameObject.GetComponentInChildren<PopUpManager>();
        }
        if (popUpManager == null)
        {
            Debug.LogError("PopUpManager not found...");
        }
        if (score == null)
        {
            score = gameObject.GetComponentInChildren<Score>();
        }
        if (score == null)
        {
            Debug.LogError("Score not found...");
        }
        if (words == null)
        {
            words = gameObject.GetComponentInChildren<Words>();
        }
        if (words == null)
        {
            Debug.LogError("Words not found...");
        }
        #endregion
        NewGame(); // start new game when loading
    }

    // Update is called once per frame
    void Update()
    {
        // *******************************************************
        #region keep board data up to date
        List<TileLetter> allLetters = tileLetters.GetAllLetters();
        // refresh board
        for (int i = 0; i < board.BoardSize * 2; i++)
        {
            for (int j = 0; j < board.BoardSize * 2; j++)
            {
                lettersGrid[i][j] = ' ';
            }
        }
        foreach (TileLetter t in allLetters)
        {
            if (t.currentPos == null) { continue; }
            Vector3 pos = (Vector3)t.currentPos;
            lettersGrid[(int)pos.y][(int)pos.x] = t.currentLetter;
        }
        #endregion
        // *******************************************************
    }

    /// <summary>
    /// power up using a letter - called from buttons
    /// </summary>
    /// <param name="s">only the first character is read<br/>
    /// this is a string because of unity limitations</param>
    public void PowerUp(string s)
    {
        // if not local clients turn then
        // dont go foward and infrom user
        if (!localTurn)
        {
            popUpManager.ShowPopUp(6);
            return;
        }

        char c = s[0];
        switch (c)
        {
            #region steal letter [NOT IMPLEMENTED YET]
            case 'Y':
                {
                    if (tileLetters.PlayerHasLetter(c))
                    {
                        Debug.LogWarning("Y Power Up Not Implemented Yet!");
                        tileLetters.RemovePlayerLetter(c);
                    }
                    else
                    {
                        popUpManager.ShowPopUp(2);
                    }
                    break;
                }
            #endregion
            #region reveal best word [NOT IMPLEMENTED YET]
            case 'Q':
                {
                    if (tileLetters.PlayerHasLetter(c))
                    {
                        string bestWord = tileLetters.BestPossiblePlay();
                        string message = "The BEST word you can make is : " + bestWord;
                        popUpManager.ShowPopUp(3, message);
                        tileLetters.RemovePlayerLetter(c);
                    }
                    else
                    {
                        popUpManager.ShowPopUp(2);
                    }
                    break;
                }
            #endregion
            #region double points
            case 'J':
                {
                    if (tileLetters.PlayerHasLetter(c))
                    {
                        pointsMultiplier += 2;
                        tileLetters.RemovePlayerLetter(c);
                    }
                    else
                    {
                        popUpManager.ShowPopUp(2);
                    }
                    break;
                }
            #endregion
            #region block tile from opponent [NOT IMPLEMENTED YET]
            case 'V':
                {
                    if (tileLetters.PlayerHasLetter(c))
                    {
                        tileLetters.RemovePlayerLetter(c);
                    }
                    else
                    {
                        popUpManager.ShowPopUp(2);
                    }
                    Debug.LogWarning("V Power Up Not Implemented Yet!");
                    break;
                }
            #endregion
            #region tripple points
            case 'Z':
                {
                    if (tileLetters.PlayerHasLetter(c))
                    {
                        tileLetters.RemovePlayerLetter(c);
                        pointsMultiplier += 3;
                    }
                    else
                    {
                        popUpManager.ShowPopUp(2);
                    }
                    break;
                }
            #endregion
            #region draw extra letter
            case 'X':
                {
                    if (tileLetters.PlayerHasLetter(c))
                    {
                        tileLetters.RemovePlayerLetter(c);
                        tileLetters.SpawnPlayerLetters(startingLettersAmount);
                    }
                    else
                    {
                        popUpManager.ShowPopUp(2);
                    }
                    break;
                }
            #endregion
            #region new set of letters
            case 'P':
                {
                    if (tileLetters.PlayerHasLetter(c))
                    {
                        RefreshLetters();
                    }
                    else
                    {
                        popUpManager.ShowPopUp(2);
                    }
                    break;
                }
            #endregion
            default:
                {
#if UNITY_EDITOR
                    Debug.LogError("No such power up - " + c);
#endif
                    break;
                }
        }
    }

    /// <summary>
    /// starts a new game
    /// </summary>
    public void NewGame()
    {
        isMultiplayer = false;
        tileLetters.DestroyAllLetters();


        lettersGrid = new List<List<char>>();
        for (int i = 0; i < board.BoardSize * 2; i++)
        {
            lettersGrid.Add(new List<char>());
            for (int j = 0; j < board.BoardSize * 2; j++)
            {
                lettersGrid[i].Add(' ');
            }
        }

        allPreviousWords = new List<string>();
        pointsMultiplier = 1;
        LocalCanPlay(false);
         
        tileLetters.ResetLettersBag();

        Invoke(nameof(NewLetters), 0.1f); // Why the fuck this work makes ZERO SENSE

        score.Reset();
        if(ChatManager.Singleton != null)
        {
            ChatManager.Singleton.ClearAllMessages();
            // Doesnt matter on start / initialisation as there are not chats in the initialisation stage ):
        }
    }

    /// <summary>
    /// refreshes the letters of the local client on start <br/>
    /// unity was not liking a function being called so delayed here
    /// </summary>
    private void NewLetters()
    {
        // UNITY being a %$&! ... - Do not remove this code
        tileLetters.SpawnPlayerLetters(startingLettersAmount);
        Invoke(nameof(tileLetters.Retrieve), 0.1f);
    }

    /// <summary>
    /// play button for the local client
    /// </summary>
    public void Play()
    {
        List<string> allWords = words.ConvertCharBoardToWords(lettersGrid);
        bool allValid = true;

        // check all words on board are valid
        string lastInvalidWord = "";
        foreach (string word in allWords)
        {
            //Debug.Log(word);
            bool isWord = words.isWord(word);
            //Debug.Log(word + " == " + isWord);
            if (!isWord) 
            { 
                allValid = false;
                lastInvalidWord = word;
            }
        }

        //Debug.Log("All words valid == " + allValid.ToString());

        if (allValid)
        {
            bool newPlay = false;
            foreach(string word in allWords)
            {
                if (!allPreviousWords.Contains(word))
                {
                    newPlay = true;
                }
            }
            
            if(newPlay)
            {
                // stop all letters played from moving if valid
                // no longer players letters as its now played
                List<TileLetter> allLetters = tileLetters.GetAllLetters();
                foreach (TileLetter t in allLetters)
                {
                    if (t.currentPos == null) { continue; }
                    if (t.GetDragDrop().dropHolder == null) { continue; }
                    t.SetPlayable(false);

                    tileLetters.SpawnPlayerLetters(startingLettersAmount);
                }

                // score
                int scoreToAdd = 0;
                foreach (string word in allWords)
                {
                    if (!allPreviousWords.Contains(word))
                    {
                        scoreToAdd += Score.CalculateBaseScore(word) * pointsMultiplier;
                    }
                }
                score.ChangeOwnScore(scoreToAdd);

                // reset variables
                pointsMultiplier = 1;
                allPreviousWords = allWords;
                LocalCanPlay(false);

                // play here - server code
                networkManager.ClientPlays(lettersGrid, scoreToAdd);
            }
            else
            {
                popUpManager.ShowPopUp(1); // ask if passing
            }
        }
        else
        {
            // prompt user with the invalid word
            string message = "Invalid Word!\n" + lastInvalidWord;
            popUpManager.ShowPopUp(0, message);
        }
    }

    /// <summary>
    /// pass button for local client
    /// </summary>
    public void Pass()
    {
        LocalCanPlay(false);
        RefreshLetters(); // refresh the clients letters for new ones
        networkManager.ClientPlays(lettersGrid, 0);
    }

    /// <summary>
    /// refreshes the clients letters
    /// </summary>
    private void RefreshLetters()
    {
        tileLetters.BagPlayersLetters();
        Invoke(nameof(SpawnPLettersDelayed), 0.25f); // Not sure why but the letters spawn were being deleted so dirty fix with a delay...
    }

    /// <summary>
    /// another issue with letters - calls function to allow delay
    /// </summary>
    private void SpawnPLettersDelayed()
    {
        tileLetters.SpawnPlayerLetters(startingLettersAmount);
        tileLetters.Retrieve();
    }

    /// <summary>
    /// opponet plays. finds new letters and adds them to board
    /// <br/>updates score accordingly
    /// </summary>
    /// <param name="lettersGridData">new board data</param>
    /// <param name="oppPoints">opponents points scored</param>
    public void OppPlay(List<List<char>> lettersGridData, int oppPoints)
    {
        List<string> allWords = words.ConvertCharBoardToWords(lettersGridData);

        // spawn new letters on board
        for(int i = 0; i < lettersGridData.Count; i++)
        {
            for (int j = 0; j < lettersGridData[i].Count; j++)
            {
                if (lettersGrid[i][j] != lettersGridData[i][j])
                {
                    tileLetters.SpawnOppLetterOnBoard(lettersGridData[i][j], j, i);
                }
            }
        }

        // check if passing
        bool newPlay = false;
        foreach (string word in allWords)
        {
            if (!allPreviousWords.Contains(word))
            {
                newPlay = true;
            }
        }

        if (newPlay)
        {
            // stop all letters played from moving if valid
            // not letters accessible by players now
            List<TileLetter> allLetters = tileLetters.GetAllLetters();
            foreach (TileLetter t in allLetters)
            {
                if (t.currentPos == null) { continue; }
                if (t.GetDragDrop().dropHolder == null) { continue; }
                t.SetPlayable(false);

            }

            // update score
            score.ChangeOppScore(oppPoints);
            allPreviousWords = allWords;

            // Show your turn
            popUpManager.ShowPopUp(4);
            LocalCanPlay(true);
        }
        else
        {
            // Show Opp passed popup
            popUpManager.ShowPopUp(5);
            LocalCanPlay(true);
        }
        

    }

    /// <summary>
    /// sets the local clients ability to be able to play or not
    /// <br/>allows for turn based gameplay
    /// </summary>
    /// <param name="play">local turn yes or no</param>
    public void LocalCanPlay(bool play)
    {
        localTurn = play;
        playButton.interactable = play;
    }

    #region draw

    /// <summary>
    /// called in unity to confirm if client wishes to draw
    /// </summary>
    public void DrawButton()
    {
        // prompt user to confirm draw
        popUpManager.ShowPopUp(9);
    }

    /// <summary>
    /// confirmed local client wishes to extend and offer to draw<br/>
    /// sends an offer to other player and waits
    /// </summary>
    public void DrawButtonCONFIRM()
    {
        popUpManager.ShowPopUp(10); // waiting for response pop up
        playButton.interactable = false;

        // ********** SEND DRAW PROMPT TO SERVER *****************
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.sendDrawPrompt);
        NetworkManager.Singleton.Client.Send(message);
    }

    /// <summary>
    /// draw accepted by local client
    /// </summary>
    public void AcceptDraw()
    {
        // button pressed saying confirm draw request
        DrawAccepted();

        // ********** SEND DRAW ACCEPTED TO SERVER *****************
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.sendDrawReply);
        message.Add(true);
        NetworkManager.Singleton.Client.Send(message);
    }

    /// <summary>
    /// draw declined by local client
    /// </summary>
    public void DeclineDraw()
    {
        // button pressed
        playButton.interactable = true;


        // ********** SEND DRAW DECLINED TO SERVER *****************
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.sendDrawReply);
        message.Add(false);
        NetworkManager.Singleton.Client.Send(message);
    }

    /// <summary>
    /// triggers a draw request from opponent
    /// </summary>
    /// <param name="message"></param>

    [MessageHandler((ushort)ServerToClientId.recieveDrawPrompt)]

    public static void DrawRequestFromOpp(Message message)
    {
        // recieve draw request from opp and display prompt
        Singleton.playButton.interactable = false;
        Singleton.popUpManager.ShowPopUp(11);

        // ********** RECIEVE DRAW PROMPT FROM SERVER *****************
    }


    /// <summary>
    /// triggers a draw response from opponent
    /// </summary>
    /// <param name="message"></param>
    
    [MessageHandler((ushort)ServerToClientId.recieveDrawReply)]

    public static void GetDrawResult(Message message)
    {
        bool result = message.GetBool();
        if (result) DrawAccepted();
        else DrawDeclined();
    }

    /// <summary>
    /// draw accepted from opponent
    /// </summary>
    public static void DrawAccepted()
    {
        // this is recieved by the server/self to tell this player the draw is done
        Singleton.popUpManager.HideAllPopUps();
        Singleton.GameOver("Draw!");

        // ********** RECIEVE DRAW ACCEPTED FROM SERVER *****************
    }

    /// <summary>
    /// draw declined by opponent
    /// </summary>
    public static void DrawDeclined()
    {
        Singleton.playButton.interactable = true;
        Singleton.popUpManager.HideAllPopUps();

        // ********** RECIEVE DRAW DECLINED FROM SERVER *****************
    }
    #endregion

    #region resign | win | lose
    /// <summary>
    /// shows pop up confirming client wishes to resign
    /// </summary>
    public void ResignButton()
    {
        popUpManager.ShowPopUp(12);
    }

    /// <summary>
    /// client resigns - send to opponent - back to matchmaking
    /// </summary>
    public void Resign()
    {
        GameOver("Opponent Wins!");

        // ********** SEND RESIGN TO SERVER *****************
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.sendResignation);
        NetworkManager.Singleton.Client.Send(message);
    }

    /// <summary>
    /// recieves from server if client wins
    /// </summary>
    /// <param name="_message"></param>
    [MessageHandler((ushort)ServerToClientId.recieveResignation)]

    public static void Win(Message _message)
    {
        // server will call this when opponent resigns
        Singleton.GameOver("You Win!");

        // ********** RECIEVE RESIGN TO SERVER *****************
    }

    #endregion

    /// <summary>
    /// ends the game and send you to the lobby
    /// </summary>
    /// <param name="win">message added to "Game Over..."</param>
    public void GameOver(string win)
    {
        string message = "Game Over...\n" + win;
        popUpManager.ShowPopUp(7, message);
        WaitForGame.waitForGame.LobbyLoad();
        Invoke(nameof(ShowLobby), 5f);
    }

    /// <summary>
    /// return client to lobby
    /// </summary>
    void ShowLobby()
    {
        mainWindowManager.ShowWindow(waitforgame);
    }
}


// *******************************************
// custom editor below - wont be in any builds
// *******************************************

#if UNITY_EDITOR
[CustomEditor(typeof(Game))]
public class EDITOR_Game : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(10);
        Game game = (Game)target;

        if(GUILayout.Button("Allow Local To Play"))
        {
            game.LocalCanPlay(true);
        }
        if(GUILayout.Button("Test"))
        {
            List<List<char>> test = new List<List<char>>();
            for(int r = 0; r < 10; r++)
            {
                var row = new List<char>();
                test.Add(row);
                for(int i =0; i<10; i++)
                {
                    row.Add(' ');
                }
            }

            test[0][1] = 'T';
            test[0][2] = 'E';
            test[0][3] = 'S';
            test[0][4] = 'T';

            game.OppPlay(test, 10);
        }
        if(GUILayout.Button("GameOver"))
        {
            game.GameOver("YOU");
        }
    }
}
#endif

