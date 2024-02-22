using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Broadcast;
using FishNet.Connection;
using TMPro;
using FF;

public class ChatHandler : MonoBehaviour
{
    public Transform chatHolder;
    public GameObject msgElement;
    //public TextMeshPro messageText;
    public TMP_InputField playerUsername, playerMessage;


    private void OnEnable()
    {
        InstanceFinder.ClientManager.RegisterBroadcast<Message>(OnMessageRecieved);
        InstanceFinder.ServerManager.RegisterBroadcast<Message>(OnClientMessageRecieved);

    }
    private void OnDisable()
    {
        InstanceFinder.ClientManager.UnregisterBroadcast<Message>(OnMessageRecieved);
        InstanceFinder.ServerManager.UnregisterBroadcast<Message>(OnClientMessageRecieved);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendMessage();
        }
    }

    public void SendMessage()
    {
        Message msg = new Message()
        {
            //username = playerUsername.text,
            //username = DataManager.Instance.name,
            username = "username",
            message = playerMessage.text
        };

        playerMessage.text = "";

        if (InstanceFinder.IsServer)
            InstanceFinder.ServerManager.Broadcast(msg);
        else if (InstanceFinder.IsClient)
            InstanceFinder.ClientManager.Broadcast(msg);
    }

    private void OnMessageRecieved(Message msg)
    {
        GameObject finalMessage = Instantiate(msgElement, chatHolder);
        //finalMessage.GetComponent<TextMeshProUGUI>().text = msg.username + ": " + msg.message;
       finalMessage.GetComponent<messageTextHolder>().msgText.text = msg.message;

    }

    private void OnClientMessageRecieved(NetworkConnection networkConnection, Message msg)
    {
        InstanceFinder.ServerManager.Broadcast(msg);
    }

    public struct Message : IBroadcast
    {
        public string username;
        public string message;
    }
}
