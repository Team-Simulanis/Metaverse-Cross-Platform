using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PreviousNextPanel : MonoBehaviour
{
    private int childCount;
    public GameObject proceedButton;
    private int timesNextButtonPressed;
    [SerializeField] bool clickOnlyCertainTimes = false;
    private void OnEnable() 
    {
        childCount = transform.childCount; 
    }
    public void _previousNextPanel(bool next)
    {
        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
                if (next){_next(i);}
                else {_previous(i);}
                break;
            }
        }
    }
    void _next(int i)
    {
        int nextIndex = (i + 1) % childCount;
        transform.GetChild(nextIndex).gameObject.SetActive(true);
        for (int j = 0; j < childCount; j++)
            {
                if (j != nextIndex)
                    {
                        transform.GetChild(j).gameObject.SetActive(false);
                    }
            }
        timesNextButtonPressed++;
        if (clickOnlyCertainTimes)
        {
            if (timesNextButtonPressed == childCount-1)
            {
                if (proceedButton != null)
                {
                    proceedButton.SetActive(true);
                }
            }
        }
    }
    void _previous(int i)
    {
        int previousIndex = (i - 1 + childCount) % childCount;
        transform.GetChild(previousIndex).gameObject.SetActive(true);
        for (int j = 0; j < childCount; j++)
            {
                if (j != previousIndex)
                    {
                        transform.GetChild(j).gameObject.SetActive(false);
                    }
            }
    }
}

