using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SendText : NetworkBehaviour
{
    [SerializeField] string Message;
    [SerializeField] bool SendMessage;
    [SerializeField] TextMeshProUGUI message;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SendMessage)
        {
            sendMessage("some message");
            SendMessage = false; 
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void sendMessage(string msg)
    {
        Message = msg;
        message.text = msg;
    }
}
