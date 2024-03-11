using Byn.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WebRTCAudioManager : MonoBehaviour
{
    public ConferenceApp ConferenceApp;
    // Start is called before the first frame update
    void Start()
    {
        //base.Start();

        StartConference("abc");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   /// <summary>
   /// Call this function to start/join Conference Call
   /// </summary>
   /// <param name="roomName"> Name of the Room</param>
    public void StartConference(string roomName)
    {
        //if (string.IsNullOrEmpty(roomName)) roomName = "Test";
        //base.AudioToggle(true);
        //base.uRoomName.text = roomName;
        //base.JoinButtonPressed(); 
        if (string.IsNullOrEmpty(roomName)) roomName = "Test";
        ConferenceApp.AudioToggle(true);
        ConferenceApp.uRoomName.text = roomName;
        ConferenceApp.JoinButtonPressed();
    }
    /// <summary>
    /// For Mute Unmute
    /// </summary>
    /// <param name="isMute"></param>
    public void MuteUnmute(bool isMute)
    {
        ConferenceApp.mCall.SetMute(isMute);
    }
    /// <summary>
    /// Call this to stop Conference Call
    /// </summary>
    public void DisconnectCall()
    {
        ConferenceApp.ShutdownButtonPressed();
    }

}
