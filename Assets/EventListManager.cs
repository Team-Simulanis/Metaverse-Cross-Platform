using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class EventListManager : MonoBehaviour
{
    public static EventListManager Instance;
     public EventsListPayload eventsListPayload;

     private void Awake()
     {
         Instance = this;
     }

     [Button]
    public async void FetchEvent()
    {
        var t = await WebRequestManager.GetWebRequestWithAuthorization("https://metaverse-backend.simulanis.io/api/application/event/list/ongoing","");
        eventsListPayload = JsonUtility.FromJson<EventsListPayload>(t); 
    }
    
    
}
