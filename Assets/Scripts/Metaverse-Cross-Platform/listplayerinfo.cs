using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class listplayerinfo : MonoBehaviour
{
    public static listplayerinfo instance;
    public static int noOfPlayer;
    public  List<playerInfo> playerList = new List<playerInfo>();
    [SerializeField] TextMeshProUGUI AvailablePlayer;
    void Start()
    {
        instance = this;
    }
    public void addNewPlayer(int id, string name) //whenever a new player spawns , add that player to the list
    {
        playerList.Add(new playerInfo(id,name));  
        updatePlayerList();
    }
    public void removeNewPlayer(int id, string name) //whenever a player despawns removes that player from the list
    {
        playerList.Remove(new playerInfo(id, name));
        updatePlayerList();
    }
    void updatePlayerList() //shows no of player , should be called wheneverr a player is spawning or despawning
    {
        noOfPlayer = playerList.Count;
        AvailablePlayer.text = noOfPlayer.ToString();
        ChatHandler.Instance.setOnlinePlayers();
    }
}
[System.Serializable]
public struct playerInfo //contains the info of player thats added to the game 
{
     public int id;
     public string name;

    public playerInfo(int _id,string _name)
    {
        id = _id;
        name = _name;
    }
}
