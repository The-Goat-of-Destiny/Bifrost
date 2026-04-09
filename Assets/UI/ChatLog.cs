using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatLog : MonoBehaviour
{
    [SerializeField] private Message MessagePrefab;
    [SerializeField] private Transform Layout;
    [SerializeField] private TMP_Text ChatWindow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void NewMessage(string Sender="", string Body = "")
    {
        Message newMessage = Instantiate(MessagePrefab, Layout);
        newMessage.RefreshVisuals(Sender, Body);
    }

    public void NewMessage(string Body = "")
    {
        NewMessage("", Body);
    }

    public void NewMessage()
    {
        // Text evidently always has 1 more letter than expected, so checking if the length is greater than 1 means it is not empty
        if (ChatWindow.text.Length > 1)
        {
            NewMessage("", ChatWindow.text);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
