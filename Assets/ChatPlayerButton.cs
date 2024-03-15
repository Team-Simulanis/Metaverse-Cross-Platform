using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.UIManager.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatPlayerButton : MonoBehaviour
{
    public TMP_Text playerName;
    public int id;

    public void OnClick()
    {
        ChatManager.Instance.selectedPlayer = id;
        ChatManager.Instance.OpenChatContainer();
        
    }
}
