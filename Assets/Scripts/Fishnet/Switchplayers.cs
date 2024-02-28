using FishNet.Component.Spawning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;

[RequireComponent(typeof(PlayerSpawner))]
public class Switchplayers : MonoBehaviour
{
    public GameObject desktopPlayerPrefab;
    public GameObject vrPlayerPrefab;
    public PlayerSpawner playerSpawner;
    //public XRGeneralSettings xrSettings = XRGeneralSettings.Instance;
    public enum platform {Desktop,VR,Mobile};

    public  platform _platform;
    private void Awake()
    {
        playerSpawner = GetComponent<PlayerSpawner>();
        if (_platform == platform.Desktop )
        {
           
        }
    }

}
