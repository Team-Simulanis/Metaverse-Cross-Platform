using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionPanel : MonoBehaviour
{
    public SessionCard sessionPrefab;
    public Transform sessionParent;
    public int noOfCards;

    [Button]
    public void PopulateEvents()
    {
        for(int i = 0; i < noOfCards; i++) 
        {
            var sessionCards = Instantiate(sessionPrefab,sessionParent);
        }
        sessionPrefab.gameObject.SetActive(false); //remove this line later
    }
    
    
}
