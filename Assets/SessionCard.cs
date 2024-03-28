
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class SessionCard : MonoBehaviour
{
    public TMP_Text sessionName;
    public TMP_Text sessionDetail;
    public TMP_Text sessionOrganiser;
    public TMP_Text sessionStatus;

   [ReadOnly] public Event eventDetails;
    public void FillData(Event details)
    {
        eventDetails = details;
        sessionName.text = details.name;
        sessionDetail.text = details.description;
        sessionOrganiser.text = details.organizer.name;
        sessionStatus.text = details.status;
    }
        
    
}
