using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsPanel : MonoBehaviour
{

    public GameObject[] mobilePanel;
    public GameObject[] desktopPanel;

    private void Start()
    {
        SwitchControlsPanels();
    }
    public void SwitchControlsPanels()
    {
#if UNITY_ANDROID
        for(int i = 0; i < mobilePanel.Length; i++) 
        {
            mobilePanel[i].SetActive(true);
            desktopPanel[i].SetActive(false);
        }
#endif
        for(int i = 0; i < desktopPanel.Length; i++)
        {
            desktopPanel[i].SetActive(true);
            mobilePanel[i].SetActive(false);
        }
    }
}
