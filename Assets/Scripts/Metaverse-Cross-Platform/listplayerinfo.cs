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
    public Dictionary<int, playerInfo> playerList = new Dictionary<int, playerInfo>();

    void Start()
    {
        instance = this;
    }

    public string message;

    public int selectedPlayer;


    public void addNewPlayer(int id, string name,
            NetworkObject connection,bool isLocalPlayer) //whenever a new player spawns , add that player to the list
    {
        playerList.Add(id,new playerInfo(id, name, connection));
        
        if(!isLocalPlayer)
        {
            ChatManager.Instance.AddPlayer(name, id);
            
        }
        else
        {
            ChatManager.Instance.myID = id;
        }
        updatePlayerList();
    }

    public void
        removeNewPlayer(int id, string name,
            NetworkObject connection) //whenever a player despawns removes that player from the list
    {
        playerList.Remove(id);
        updatePlayerList();
        ChatManager.Instance.RemovePlayer(id);
        
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