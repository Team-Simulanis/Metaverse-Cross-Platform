using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class ListPlayerInfo : MonoBehaviour
{
    public static ListPlayerInfo Instance;
    public static int NoOfPlayer;
    public Dictionary<int, PlayerInfo> playerList = new();

    private void Start()
    {
        Instance = this;
    }

    public string message;

    public int selectedPlayer;

    //whenever a new player spawns , add that player to the list
    public void AddNewPlayer(int id, string pName, NetworkObject connection, bool isLocalPlayer)
    {
        playerList.Add(id, new PlayerInfo(id, pName, connection));

        if (!isLocalPlayer)
        {
            ChatManager.Instance.AddPlayer(pName, id);
        }
        else
        {
            ChatManager.Instance.myID = id;
        }

        UpdatePlayerList();
    }

    //whenever a player de-spawns removes that player from the list
    public void RemoveNewPlayer(int id)
    {
        playerList.Remove(id);
        UpdatePlayerList();
        if (ChatHandler.Instance)
            ChatManager.Instance.RemovePlayer(id);

        Debug.Log("removed");
    }

    private void UpdatePlayerList() //shows no of player , should be called whenever a player is spawning or de-spawning
    {
        NoOfPlayer = playerList.Count;
        if (ChatHandler.Instance != null)
            ChatHandler.Instance.setOnlinePlayers();
    }
}

[System.Serializable]
public class PlayerInfo //contains the info of player that's added to the game 
{
    public int id;
    public string name;
    public NetworkObject connection;

    public PlayerInfo(int id, string name, NetworkObject connection)
    {
        this.id = id;
        this.name = name;
        this.connection = connection;
    }
}