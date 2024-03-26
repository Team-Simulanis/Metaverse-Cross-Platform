using System.Collections.Generic;
using Doozy.Runtime.UIManager.Containers;
using FishNet.Connection;
using Sirenix.OdinInspector;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    public static ChatManager Instance;
    public ChatPlayerButton chatPlayerButtonPrefab;
    public GameObject chatPlayerButtonParent;
    public int myID;
    public Dictionary<int, ChatPlayerButton> chatPlayerButtons = new Dictionary<int, ChatPlayerButton>();
    
   
    public string message;
    public int selectedPlayer;
    
    public UIContainer privateChatContainer;
    public UIView chatPanel;
    
    
    private void Awake()
    {
        Instance = this;
    }
    public void OpenChatContainer()
    {
        privateChatContainer.Show();
        ChatHandler.Instance.isPrivate = true;
    }
    
    public void CloseChatContainer()
    {
        privateChatContainer.Hide();
        ChatHandler.Instance.isPrivate = false;
    }

    [Button]
    public void SendPrivateMessage()
    {
        NetworkController.Instance.SendPrivateChatServer(new NetworkConnection()
        {
            ClientId = selectedPlayer
        }, ListPlayerInfo.Instance.playerList[selectedPlayer].name, message,myID);
    }

    public new void SendPrivateMessage(string msg)
    {
        NetworkController.Instance.SendPrivateChatServer(new NetworkConnection()
        {
            ClientId = selectedPlayer
        }, ListPlayerInfo.Instance.playerList[selectedPlayer].name, msg,myID);
    }

    public void AddPlayer(string userName, int id)
    {
        var cp = Instantiate(chatPlayerButtonPrefab, chatPlayerButtonParent.transform);

        cp.playerName.text = userName;
        cp.id = id;

        chatPlayerButtons.Add(id,cp);
    }

    public void RemovePlayer(int id)
    {
        var chatPlayerButton = chatPlayerButtons[id];
        if(chatPlayerButtons.ContainsKey(id))
        {
            chatPlayerButtons.Remove(id);
            Destroy(chatPlayerButton.gameObject);
        }
    }
}