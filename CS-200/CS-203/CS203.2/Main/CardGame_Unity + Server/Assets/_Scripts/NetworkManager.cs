using Riptide;
using Riptide.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#region using editor
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

#region network related enums
public enum ClientToServerId : ushort
{
    sendName = 1,
    sendSearchForMatch = 2,
    sendTurnToServer = 3,
    sendChat = 4,
    sendResignation = 5,
    sendDrawPrompt = 6,
    sendDrawReply = 7,
}
public enum ServerToClientId : ushort
{
    recieveGameStarted = 1,
    recieveBoardState = 2,
    recieveChat = 4,
    recieveOpponentDisconnect = 5,
    recieveResignation = 6,
    recieveDrawPrompt = 7,
    recieveDrawReply = 8,
}
#endregion

public class NetworkManager : MonoBehaviour
{
    #region temp editor functions
    #if UNITY_EDITOR
    public void TempLocalSever()
    {
        ip = "192.168.1.77";
    }
    public void TempGlobalSever()
    {
        ip = "219.89.18.156";
    }
    #endif
    #endregion

    private static NetworkManager _singleton;

    /// <summary>
    /// ensures there is one of this object open at a time (destroys others)
    /// </summary>
    public static NetworkManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(NetworkManager)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }

    #region local variables
    [SerializeField] private string ip;
    [SerializeField] private ushort port;
    #endregion
    
    /// <summary>
    /// gets the local client
    /// </summary>
    public Client Client { get; private set; }

    // Awake is called when the script is loaded
    private void Awake()
    {
        Singleton = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        Client = new Client();
        Client.Connected += DidConnect;
        Client.ConnectionFailed += FailedToConnect;
        Client.Disconnected += DidDisconnect;
    }

    // FixedUpdate is called once every fixed frame
    private void FixedUpdate()
    {
        Client.Update();
    }

    /// <summary>
    /// sends disconnect to server on quite
    /// </summary>
    private void OnApplicationQuit()
    {
        Client.Disconnect();
    }

    /// <summary>
    /// connects to server
    /// </summary>
    public void Connect()
    {
        Client.Connect($"{ip}:{port}");
    }

    /// <summary>
    /// disconnects from server
    /// </summary>
    public void Disconnect()
    {
        Client.Disconnect();
    }

    /// <summary>
    /// sends name and connection state to server
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DidConnect(object sender, EventArgs e)
    {
        UIManager.Singleton.SendName();
        UIManager.Singleton.ConnectionSucceeded();
    }

    /// <summary>
    /// informs user the server didnt connect
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FailedToConnect(object sender, EventArgs e)
    {
        UIManager.Singleton.ConnectionFailed();
    }
    
    /// <summary>
    /// informs client when an other user disconnects
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DidDisconnect(object sender, EventArgs e)
    {
        // Popup and to main menu
        UIManager.Singleton.PlayerDisconnect();
    }

    /// <summary>
    /// client plays their turn - sends data to server
    /// </summary>
    /// <param name="board"></param>
    /// <param name="score"></param>
    public void ClientPlays(List<List<char>> board, int score)
    {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.sendTurnToServer);
        message.AddString(CompileBoard(board));
        message.AddInt(score);
        Singleton.Client.Send(message);
    }

    /// <summary>
    /// server calls this when recieving turn from other player
    /// </summary>
    /// <param name="message"></param>
    [MessageHandler((ushort)ServerToClientId.recieveBoardState)]
    public static void OppenentPlays(Message message)
    {
        string boardRAW = message.GetString();
        int score = message.GetInt();

        List<List<char>> boardP = new List<List<char>>();

        for (int i = 0; i < Game.Singleton.board.BoardSize * 2; i++)
        {
            List<char> row = new List<char>();
            for (int j = 0; j < Game.Singleton.board.BoardSize * 2; j++)
            {
                row.Add(' ');
            }
            boardP.Add(row);
        }

        List<string> temp = boardRAW.Split('\n').ToList();
        for (int i = 0; i < temp.Count; i++)
        {
            for (int j = 0; j < temp[i].Count(); j++)
            {
                boardP[i][j] = temp[i][j];
            }
        }

        Game.Singleton.OppPlay(boardP, score);
    }

    /// <summary>
    /// server calls this when opponent leaves the match in progress
    /// </summary>
    /// <param name="message"></param>
    [MessageHandler((ushort)ServerToClientId.recieveOpponentDisconnect)]
    public static void OpponentDisconnect(Message message)
    {
        //alex stuff
        Game.Singleton.LocalCanPlay(false);
        Game.Singleton.popUpManager.ShowPopUp(8);
        Game.Singleton.isMultiplayer = false;
    }

    /// <summary>
    /// convers string from data board to a string for server
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    private static string CompileBoard(List<List<char>> board)
    {
        string output = "";
        foreach (var row in board)
        {
            foreach (char  c in row)
            {
                output += c;
            }
            output += "\n";
        }
        return output;
    }
}


// *******************************************
// custom editor below - wont be in any builds
// *******************************************

#if UNITY_EDITOR
[CustomEditor(typeof(NetworkManager))]
public class EDITOR_NetworkManager : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        NetworkManager nm = (NetworkManager)target;
        if(GUILayout.Button("temp local"))
        {
            nm.TempLocalSever();
        }
        if (GUILayout.Button("temp global"))
        {
            nm.TempGlobalSever();
        }
    }
}
#endif