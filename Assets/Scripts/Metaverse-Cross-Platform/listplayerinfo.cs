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
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addNewPlayer(int id, string name)
    {
        playerList.Add(new playerInfo(id,name));  
        noOfPlayer = playerList.Count;
        AvailablePlayer.text = noOfPlayer.ToString();
        ChatHandler.Instance.setOnlinePlayers();
    }

    public void removeNewPlayer(int id, string name) 
    {
        playerList.Remove(new playerInfo(id, name));
        noOfPlayer = playerList.Count;
        AvailablePlayer.text = noOfPlayer.ToString();
        ChatHandler.Instance.setOnlinePlayers();
    }

}

[System.Serializable]
public struct playerInfo
{
     public int id;
     public string name;

    public playerInfo(int _id,string _name)
    {
        id = _id;
        name = _name;
    }
}
