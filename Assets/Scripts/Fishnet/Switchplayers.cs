using FishNet.Managing;
using FishNet.Object;
using UnityEngine;
using UnityEngine.Networking;
public class Switchplayers : MonoBehaviour
{
    public GameObject desktopPlayer;
    public GameObject vrPlayer;
    //NetworkManager networkManager;
    public enum platform {Desktop,VR,Mobile};

    public  platform _platform;
    private void OnEnable()
    {
       
    }
    
   
}
