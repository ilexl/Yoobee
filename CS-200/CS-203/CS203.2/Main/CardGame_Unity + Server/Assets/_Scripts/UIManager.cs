using Riptide;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region local variables
    [Header("Connect")]
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private PopUpManager connectPopUpManager;
    [SerializeField] private WindowManager windowManager;
    [SerializeField] private Window WaitingForGame;
    [SerializeField] private Window MultiplayerConnect;
    [SerializeField] private Window Game;
    private static UIManager _singleton;
    #endregion

    /// <summary>
    /// ensures there is one of this object open at a time (destroys others)
    /// </summary>
    public static UIManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(UIManager)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }

    // Awake is called when the script is being loaded
    private void Awake()
    {
        Singleton = this;
    }

    /// <summary>
    /// allows the user to edit their username
    /// </summary>
    public void EnableUsernameEdit()
    {
        usernameField.interactable = true;
    }

    /// <summary>
    /// connects to server
    /// </summary>
    public void ConnectClicked()
    {
        usernameField.interactable = false;
        NetworkManager.Singleton.Connect();
    }
    
    /// <summary>
    /// searches for match
    /// </summary>
    public void SearchClicked()
    {
        SendSearchStatus();
    }

    /// <summary>
    /// disconnects from server
    /// </summary>
    public void Disconnect()
    {
        NetworkManager.Singleton.Disconnect();
    }

    /// <summary>
    /// sends status to server for searching
    /// </summary>
    private void SendSearchStatus()
    {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.sendSearchForMatch);
        NetworkManager.Singleton.Client.Send(message);
    }
    
    /// <summary>
    /// informs user that server is unreachable
    /// </summary>
    public void ConnectionFailed()
    {
        usernameField.interactable = true;
        connectPopUpManager.ShowPopUp(0);
    }

    /// <summary>
    /// navigate to correct window when connected
    /// </summary>
    public void ConnectionSucceeded()
    {
        windowManager.ShowWindow(WaitingForGame);
    }

    /// <summary>
    /// sends name to server
    /// </summary>
    public void SendName()
    {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.sendName);
        message.AddString(usernameField.text);
        NetworkManager.Singleton.Client.Send(message);
    }

    /// <summary>
    /// informs user the other player disconnected
    /// </summary>
    public void PlayerDisconnect()
    {
        windowManager.ShowWindow(MultiplayerConnect);
        connectPopUpManager.ShowPopUp(1);
    }

    /// <summary>
    /// sends a chat from client to opponent through server
    /// </summary>
    /// <param name="chatMessage"></param>
    public void SendChatMessage(string chatMessage)
    {
        // Will To send message here to the other player from the server
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.sendChat);
        message.AddString(chatMessage);
        NetworkManager.Singleton.Client.Send(message);
        // Alex creates chatMessage under here for GUI to display
        ChatManager.Singleton.Message(chatMessage, ChatManager.SentBy.Player);
    }


    /// <summary>
    /// recieves a chat from opponent via server
    /// </summary>
    [MessageHandler((ushort)ServerToClientId.recieveChat)]

    public static void RecieveChatMessage(Message message)
    {
        Singleton.UpdateChat(message.GetString());
    }

    /// <summary>
    /// updates the chat of the new message
    /// </summary>
    /// <param name="message"></param>
    public void UpdateChat(string message)
    {
        // Will to call this function when recieving a message from the server/other player

        // Alex creates message under here for GUI to display
        ChatManager.Singleton.Message(message, ChatManager.SentBy.Opponent);
    }
} 
