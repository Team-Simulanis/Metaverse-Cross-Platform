using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class NetworkController : NetworkBehaviour
{
    public static NetworkController Instance;

    private void Awake()
    {
        Instance = this;
    }

    [ServerRpc(RequireOwnership = false)]
    public void SendPrivateChatServer(NetworkConnection networkConnection, string sender, string message,int senderID)
    {
        //Send to only the owner.
        PrivateChatClient(networkConnection, sender, message, senderID);
    }


    //Chat
    [TargetRpc]
    private void PrivateChatClient(NetworkConnection target, string sender, string message, int senderID)
    {
        Debug.Log(sender + "  :" + message);
        Debug.Log(target.ClientId + "  :" + senderID);
        ChatHandler.Instance.OnPrivateMessageReceived(new IMessage()
        {
            message = message,
            username = sender
        });
        ChatManager.Instance.chatPanel.Show();
        ChatManager.Instance.privateChatContainer.Show();
        
        ChatManager.Instance.selectedPlayer = senderID == ChatManager.Instance.myID ? target.ClientId : senderID;

        ChatHandler.Instance.isPrivate = true;
    }
}