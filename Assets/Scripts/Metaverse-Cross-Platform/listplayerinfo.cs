using FishNet.Connection;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using Sirenix.OdinInspector;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class listplayerinfo : MonoBehaviour
{
    public static listplayerinfo instance;
    public static int noOfPlayer;
    public List<playerInfo> playerList = new List<playerInfo>();

    void Start()
    {
        instance = this;
    }

    public string message;

    public int selectedPlayer;

    [Button]
    public void SendMessage()
    {
        NetworkController.Instance.SendPrivateChatServer(new NetworkConnection()
        {
            ClientId = selectedPlayer
        }, playerList[selectedPlayer].name, message);
    }

    public void addNewPlayer(int id, string name,
            NetworkObject connection) //whenever a new player spawns , add that player to the list
    {
        playerList.Add(new playerInfo(id, name, connection));
        ChatManager.Instance.AddPlayer(name, id);
        updatePlayerList();
    }

    public void
        removeNewPlayer(int id, string name,
            NetworkObject connection) //whenever a player despawns removes that player from the list
    {
        playerList.Remove(new playerInfo(id, name, connection));
        updatePlayerList();
        Debug.Log("removed");
    }

    void updatePlayerList() //shows no of player , should be called wheneverr a player is spawning or despawning
    {
        noOfPlayer = playerList.Count;
        ChatHandler.Instance.setOnlinePlayers();
    }
}

[System.Serializable]
public class playerInfo //contains the info of player thats added to the game 
{
    public int id;
    public string name;
    public NetworkObject connection;

    public playerInfo(int _id, string _name, NetworkObject _connection)
    {
        id = _id;
        name = _name;
        connection = _connection;
    }
}