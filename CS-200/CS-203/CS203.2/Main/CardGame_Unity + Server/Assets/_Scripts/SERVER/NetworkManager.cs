using Riptide;
using Riptide.Utils;
using UnityEngine;
/********************************************************************************THIS IS THE SERVERSIDE VERSION DON'T BE A FOOL ************************************************************************/
public enum ClientToServerId : ushort
{
    recieveNameFromClient = 1,
    recieveSearchForMatch = 2,
    recieveTurnFromClient = 3,
    recieveChat = 4,
    recieveResignation = 5,
    recieveDrawPrompt = 6,
    recieveDrawReply = 7,
}

public enum ServerToClientId : ushort
{
    sendGameStarted = 1,
    sendBoardStateToClient = 2,
    sendChat = 4,
    sendOpponentDisconnect = 5,
    sendResignation = 6,
    sendDrawPrompt = 7,
    sendDrawReply = 8,
}

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager _singleton;
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

    /********************************************************************************THIS IS THE SERVERSIDE VERSION DON'T BE A FOOL ************************************************************************/
    public Server Server { get; private set; }

    [SerializeField] private ushort port;
    [SerializeField] private ushort maxClientCount;
    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Server = new Server();
        Server.Start(port, maxClientCount);
        Server.ClientDisconnected += PlayerLeft;
    }
    /********************************************************************************THIS IS THE SERVERSIDE VERSION DON'T BE A FOOL ************************************************************************/
    private void FixedUpdate()
    {
        Server.Update();
    }

    private void OnApplicationQuit()
    {
        Server.Stop();
    }

    private void PlayerLeft(object sender, ServerDisconnectedEventArgs e)
    {
        Matchmaking.DestroyPlayer(e.Client.Id);
    }
    /********************************************************************************THIS IS THE SERVERSIDE VERSION DON'T BE A FOOL ************************************************************************/
}
