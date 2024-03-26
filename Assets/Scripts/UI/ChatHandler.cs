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
    public Transform privateChatHolder;
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
        InstanceFinder.ClientManager.RegisterBroadcast<IMessage>(OnMessageRecieved);
        InstanceFinder.ServerManager.RegisterBroadcast<IMessage>(OnClientMessageRecieved);
    }

    private void OnDisable()
    {
        InstanceFinder.ClientManager.UnregisterBroadcast<IMessage>(OnMessageRecieved);
        InstanceFinder.ServerManager.UnregisterBroadcast<IMessage>(OnClientMessageRecieved);
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
        onlinePlayers.text = ListPlayerInfo.NoOfPlayer.ToString() + " online";
    }

    public bool isPrivate;

    public void SendMessage()
    {
        isOwner = true;
        IMessage msg = new IMessage()
        {
            username = "username",
            message = playerMessage.text
        };

        playerMessage.text = "";
        if (isPrivate)
        {
            ChatManager.Instance.SendPrivateMessage(msg.message);
            OnPrivateMessageReceived(new IMessage()
            {
                message = msg.message,
                username = ListPlayerInfo.Instance.playerList[ChatManager.Instance.selectedPlayer].name
            });
            return;
        }

        if (InstanceFinder.IsServer)
            InstanceFinder.ServerManager.Broadcast(msg);
        else if (InstanceFinder.IsClient)
            InstanceFinder.ClientManager.Broadcast(msg);
        ChatboxContainer.verticalNormalizedPosition = -1f;
    }

    private void OnMessageRecieved(IMessage msg)
    {
        if (string.IsNullOrEmpty(msg.message)) return;
        GameObject finalMessage;
        if (isOwner)
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

    public void OnPrivateMessageReceived(IMessage msg)
    {
        Debug.Log("received private message");

        if (string.IsNullOrEmpty(msg.message))
        {
            Debug.Log("Empty private");
            return;
        }

        Debug.Log("received private message");
        GameObject finalMessage;

        if (isOwner)
        {
            finalMessage = Instantiate(ownerMessageElement, privateChatHolder);
            finalMessage.GetComponent<messageTextHolder>().msgText.text = msg.message;
            PrivateMessages.Add(finalMessage);
        }
        else
        {
            finalMessage = Instantiate(msgElement, privateChatHolder);
            finalMessage.GetComponent<messageTextHolder>().msgText.text = msg.message;
            PrivateMessages.Add(finalMessage);
        }

        isOwner = false;
    }

    public List<GameObject> PrivateMessages = new List<GameObject>();

    private void OnClientMessageRecieved(NetworkConnection networkConnection, IMessage msg)
    {
        InstanceFinder.ServerManager.Broadcast(msg);
    }
}

public struct IMessage : IBroadcast
{
    public string username;
    public string message;
}