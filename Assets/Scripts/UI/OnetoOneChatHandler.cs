using FishNet.Connection;
using FishNet.Managing;
using FishNet.Object;
using TMPro;
using UnityEngine;

public class OnetoOneChatHandler : NetworkBehaviour
{
    [SerializeField] bool sendMsg;
    [SerializeField] TextMeshProUGUI ShowMessage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(sendMsg)
        {
            MessageSent();
            sendMsg = false;
        }
    }

    [TargetRpc]
    [ObserversRpc]
    void Showchat(NetworkConnection target,string sender,string message)
    {
        if(OwnerId == 0)
        {
            ShowMessage.text = "hello";
            Debug.Log($"{sender}: {message}."+"received message");
        }
    }
    [ServerRpc]
    void SendMessage()
    {
        if(sendMsg)
        Showchat(base.Owner, "name", "message");
        Debug.Log("server");
    }

    public void MessageSent()
    {
        SendMessage();
        Debug.Log("owner id is: " + base.OwnerId+"is owner "+ IsOwner);

    }
}
