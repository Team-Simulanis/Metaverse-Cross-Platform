using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class FeedBackManager : MonoBehaviour
{
    public static FeedBackManager Instance;
    
    [ReadOnly] public string endPoint = "https://metaverse-backend.simulanis.io/api/event/feedback";

    private void Awake()
    {
        Instance = this;
    }

    public async void SendFeedBack(int rating, string feedback, string eventId)
    {
        var feedbackPayload = new FeedbackPayload()
        {
            rating = rating,
            feedback = feedback,
            eventId = eventId
        };

        var json = JsonUtility.ToJson(feedbackPayload);
        await WebRequestManager.PostWebRequestWithAuthorization(endPoint, json);
        
        Debug.Log("Submitted Feedback");
    }
}

[Serializable]
public class FeedbackPayload
{
    public int rating;
    public string feedback;
    public string eventId;
}