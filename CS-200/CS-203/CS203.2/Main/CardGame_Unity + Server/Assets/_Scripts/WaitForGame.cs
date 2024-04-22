using Riptide;
using UnityEngine;

public class WaitForGame : MonoBehaviour
{
    [SerializeField] public static WaitForGame waitForGame; // static for server interaction
    #region local variables
    [SerializeField] Game GameMain;
    [SerializeField] WindowManager windowManagerMAIN;
    [SerializeField] Window game;
    [SerializeField] Window mainMenu;
    [SerializeField] WindowManager WaitForGameWindows;
    [SerializeField] Window searchForMatch;
    [SerializeField] Window searchingForMatch;
    [SerializeField] Window matchFound;
    [SerializeField] Window waitingForServer;
    #endregion

    // Awake is called when the script is being loaded
    private void Awake()
    {
        if (waitForGame == null)
        {
            waitForGame = FindAnyObjectByType<WaitForGame>();
        }
    }

    /// <summary>
    /// entry to lobby
    /// </summary>
    public void LobbyLoad()
    {
        WaitForGameWindows.ShowWindow(searchForMatch);
    }

    /// <summary>
    /// sets game window open when game is ready
    /// </summary>
    public void GameReady()
    {
        windowManagerMAIN.ShowWindow(game);
    }

    /// <summary>
    /// searches for match
    /// </summary>
    public void SearchForMatch()
    {
        WaitForGameWindows.ShowWindow(searchingForMatch);
    }

    /// <summary>
    /// code when client disconnects from server - button included
    /// </summary>
    public void Disconnect()
    {
        windowManagerMAIN.ShowWindow(mainMenu);
        // disconnect from server
    }

    /// <summary>
    /// waits for the game to load
    /// </summary>
    public void MatchFound()
    {
        WaitForGameWindows.ShowWindow(matchFound);
    }

    /// <summary>
    /// game is loaded by the server
    /// </summary>
    /// <param name="message"></param>

    [MessageHandler((ushort)ServerToClientId.recieveGameStarted)]

    private static void RecieveGameStartCall(Message message)
    {
        
        ushort otherPlayerId = message.GetUShort();
        Game.Singleton.OtherPlayerUsername = message.GetString();
        bool DoWeStart = message.GetBool();
        Debug.Log($"Started match with player [{Game.Singleton.OtherPlayerUsername}:{otherPlayerId}]. ");
        string nextLine = "You are ";
        string nextLine2 = DoWeStart ? "" : "not ";
        nextLine += nextLine2 + "starting!";
        Debug.Log(nextLine);
        Game.Singleton.NewGame();
        Game.Singleton.isMultiplayer = true;
        waitForGame.GameReady();
        waitForGame.GameMain.LocalCanPlay(DoWeStart);
    }
}

