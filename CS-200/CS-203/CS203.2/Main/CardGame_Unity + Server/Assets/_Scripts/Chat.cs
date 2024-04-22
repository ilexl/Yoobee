using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class Chat : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI chatTxt;
    [SerializeField] RectTransform chatHeight;
    [SerializeField] private float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        // resize chat box to correct size
        if(chatTxt.fontSize < chatTxt.fontSizeMax)
        {
            List<string> chats = chatTxt.text.Split(" ").ToList();
            int index = 0;
            // Must split string up or it will expand forever...
            foreach(string chat in chats)
            {
                if(chat.Length > 35)
                {
                    string firsthalf = chat.Substring(0, 35);
                    string secondhalf = chat.Substring(36);
                    chats.RemoveAt(index);
                    chats.Insert(index, firsthalf);
                    chats.Insert(index + 1, secondhalf);

                    string nexText = string.Empty;
                    for(int i = 0; i < chats.Count(); i++)
                    {
                        nexText += chats[i];
                        if(i < chats.Count() - 1)
                        {
                            nexText += " ";
                        }
                    }
                    chatTxt.text = nexText;


                    return;
                }
                index++;
            }


            float diff = speed * Time.deltaTime;
            chatHeight.sizeDelta = new Vector2(chatHeight.rect.width, chatHeight.rect.height + diff);

            VerticalLayoutGroup vlg = gameObject.GetComponentInParent<VerticalLayoutGroup>();
            if(vlg != null)
            {
                vlg.enabled = false;
                vlg.enabled = true;
                // ensures smooth moving of the vertial layout group
            }
            FindAnyObjectByType<ChatManager>().GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
        }
    }

    /// <summary>
    /// sets the text of the chat
    /// </summary>
    /// <param name="text">text to show</param>
    public void SetText(string text)
    {
        chatTxt.text = text;
    }
}
