using UnityEngine;
using TMPro;
#region using editor
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

public class ChatManager : MonoBehaviour
{
    #region local variables
    [SerializeField] GameObject offlineOverlay;
    private static ChatManager _singleton;
    [SerializeField] GameObject playerChatPrefab;
    [SerializeField] GameObject opponentChatPrefab;
    [SerializeField] Transform parent;
    [SerializeField] Transform typeToChatHolder;
    [SerializeField] TMP_InputField blank;
    #endregion

    /// <summary>
    /// ensures there is one of this object open at a time (destroys others)
    /// </summary>
    public static ChatManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(ChatManager)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }

    /// <summary>
    /// determines who the chat was sentby
    /// </summary>
    public enum SentBy
    {
        Player, 
        Opponent,
    }

    // Awake is called when the script is loaded
    private void Awake()
    {
        Singleton = this;
    }

    // Update is called once per frame
    void Update()
    {
        // check and make sure the user can chat if they are offline
        offlineOverlay.SetActive(!Game.Singleton.isMultiplayer); 
    }

    /// <summary>
    /// removes all messages and clears the chat for a new game
    /// </summary>
    public void ClearAllMessages()
    {
        foreach(Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// was supposed to clear the chat box and lose focus but jank <br/>
    /// now deletes the chat box and replaces it with a new one
    /// </summary>
    /// <param name="inputField">to replace with new input field TMPro</param>
    public void SendMessageTMPro(TMP_InputField inputField)
    {
        if(inputField.text == null || inputField.text == string.Empty || inputField.text == "" ) { return; }
        UIManager.Singleton.SendChatMessage(inputField.text);
        inputField.gameObject.SetActive(false);
        GameObject newChatInput = Instantiate(blank.gameObject, typeToChatHolder);
        newChatInput.SetActive(true);

        foreach (Transform child in inputField.transform)
        {
            foreach (Transform child2 in child.transform)
            {
                Destroy (child2.gameObject);
            }
            Destroy(child.gameObject);
        }
        Destroy(inputField.gameObject);

    }

    /// <summary>
    /// message to display on screen
    /// </summary>
    /// <param name="message">text in the message</param>
    /// <param name="sentBy">who the message was sent by</param>
    public void Message(string message, SentBy sentBy)
    {
        if(SentBy.Player == sentBy)
        {
            GameObject newMessage = Instantiate(playerChatPrefab, parent);
            newMessage.GetComponent<Chat>().SetText(message);
        }
        else if (SentBy.Opponent == sentBy)
        {
            GameObject newMessage = Instantiate(opponentChatPrefab, parent);
            newMessage.GetComponent<Chat>().SetText(message);
        }
        else 
        {
            Debug.LogWarning("Message not created as not sent by opp or you...");
        }

    }
}

// *******************************************
// custom editor below - wont be in any builds
// *******************************************

#if UNITY_EDITOR
[CustomEditor(typeof(ChatManager))]
public class EDITOR_ChatManager : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ChatManager cm = (ChatManager)target;
        if(GUILayout.Button("Player Large Message"))
        {
            cm.Message("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It", ChatManager.SentBy.Player);

        }
        if (GUILayout.Button("Player Small Message"))
        {
            cm.Message("Test Message", ChatManager.SentBy.Player);

        }
        if (GUILayout.Button("Opponent Large Message"))
        {
            cm.Message("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It", ChatManager.SentBy.Opponent);

        }
        if (GUILayout.Button("Opponent Small Message"))
        {
            cm.Message("Test Message", ChatManager.SentBy.Opponent);

        }
    }
}
#endif