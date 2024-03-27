using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameHUDManager : MonoBehaviour
{
    public List<GameObject> offlineFeatures;
    public List<GameObject> onlineFeatures;

    public void ShowGameHUD()
    {
        Debug.Log("Game HUD is shown");
    }

    private void Start()
    {
        if(transform.parent.GetComponent<RPMPlayerManager>().playerType == PlayerType.Offline)
        {
            foreach (var feature in offlineFeatures)
            {
                feature.SetActive(true);
            }
            foreach (var feature in onlineFeatures)
            {
                feature.SetActive(false);
            }
        }
        else
        {
            foreach (var feature in onlineFeatures)
            {
                feature.SetActive(true);
            }
            foreach (var feature in offlineFeatures)
            {
                feature.SetActive(false);
            }
        }
    }
}