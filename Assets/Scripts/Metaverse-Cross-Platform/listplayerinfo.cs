using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class listplayerinfo : MonoBehaviour
{
    public static listplayerinfo instance;
    public  List<playerInfo> playerList = new List<playerInfo>();
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
    }

    public void removeNewPlayer() 
    {
        playerList.Remove(new playerInfo());
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
