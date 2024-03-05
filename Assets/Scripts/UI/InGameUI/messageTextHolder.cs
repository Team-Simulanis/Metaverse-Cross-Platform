using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class messageTextHolder : MonoBehaviour
{
    public TextMeshProUGUI username;
    public TextMeshProUGUI msgText;
    public TextMeshProUGUI TimeText;

    private void OnEnable()
    {
        ShowTimeInChat();
    }

    void ShowTimeInChat()
    {
        DateTime currentTime = System.DateTime.Now;
        int hours = currentTime.Hour;
        int minutes = currentTime.Minute;
        TimeText.text = hours + ":" + minutes;
        Debug.Log($"Current time: {hours:D2}:{minutes:D2}");
    }
}

