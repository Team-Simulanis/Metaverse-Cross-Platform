using FishNet.Connection;
using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SendText : NetworkBehaviour
{
    public static SendText instance;
    [SerializeField] string Message;
    [SerializeField] bool SendMessage;
    [SerializeField] TextMeshProUGUI message;
    [SerializeField] TMP_InputField messageInput;

    private void Start()
    {
        instance = this;
        newFunction("rizwan");
    }

    [ObserversRpc]
    private void sendMessage(string messages)
    {
        message.text = messages;
    }

    [ServerRpc (RequireOwnership =false)]
    private void SendMessageServer(string message)
    {
        sendMessage(message);
    }

    // Input field onEditEnd
    private void newFunction(string value)
    {
        SendMessageServer(value);
    }

    public void WriteMessage()
    {
        Message = messageInput.text;
        newFunction(Message);
    }

/*    private void OnValidate()
    {
        if (SendMessage)
        {
            SendMessageServer();
            SendMessage = false;
        }

    }*/
}
