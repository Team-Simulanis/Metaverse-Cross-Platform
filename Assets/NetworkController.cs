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
    public void SendPrivateChatServer(NetworkConnection networkConnection, string sender, string message)
    {
        //Send to only the owner.
        PrivateChatClient(networkConnection, sender, message);
    }
    //Chat
    [TargetRpc]
    private void PrivateChatClient(NetworkConnection target, string sender, string message)
    {
        Debug.Log(sender+"  :"+message);
        //chatManager.OnPrivateMessage(sender, message);
    }
}
