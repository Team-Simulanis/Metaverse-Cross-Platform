using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Broadcast;
using FishNet.Connection;
using TMPro;
using FF;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ChatHandler : MonoBehaviour
{
    public static ChatHandler Instance;
    public Transform chatHolder;
    public GameObject msgElement;
    public TMP_InputField playerUsername, playerMessage;
    [SerializeField] ScrollRect ChatboxContainer;
    [SerializeField] TextMeshProUGUI onlinePlayers;
    private void Start()
    {
        Instance = this;
    }
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

    public void setOnlinePlayers()
    {
        onlinePlayers.text = listplayerinfo.noOfPlayer.ToString()+" players online";
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
        ChatboxContainer.verticalNormalizedPosition = -1f;
    }

    private void OnMessageRecieved(Message msg)
    {
        if (string.IsNullOrEmpty(msg.message)) return;

        GameObject finalMessage = Instantiate(msgElement, chatHolder);
        //finalMessage.GetComponent<TextMeshProUGUI>().text = msg.username + ": " + msg.message;
       finalMessage.GetComponent<messageTextHolder>().msgText.text = msg.message;
        Debug.Log("the size is: "+ChatboxContainer.verticalNormalizedPosition);
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
