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
using FishNet.Object;
using Sirenix.Utilities;

public class ChatHandler : MonoBehaviour
{
    public static ChatHandler Instance;
    public Transform chatHolder;
    public GameObject msgElement, ownerMessageElement;
    public TMP_InputField playerUsername, playerMessage;
    [SerializeField] ScrollRect ChatboxContainer;
    [SerializeField] TextMeshProUGUI onlinePlayers;

    [SerializeField] bool isOwner;

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
        onlinePlayers.text = listplayerinfo.noOfPlayer.ToString()+" online";
    }

    public void SendMessage()
    {
        isOwner = true;
        Message msg = new Message()
        {
            username = DataManager.Instance.name,
            //username = DataManager.Instance.name,
            //username = "username",
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
        GameObject finalMessage;
        if(isOwner)
        {
            finalMessage = Instantiate(ownerMessageElement, chatHolder);
            finalMessage.GetComponent<messageTextHolder>().msgText.text = msg.message;
        }
        else
        {
            finalMessage = Instantiate(msgElement, chatHolder);
            finalMessage.GetComponent<messageTextHolder>().msgText.text = msg.message;
        }
        isOwner = false;
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
