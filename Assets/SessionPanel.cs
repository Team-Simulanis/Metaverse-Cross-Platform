using Sirenix.OdinInspector;
using UnityEngine;

public class SessionPanel : MonoBehaviour
{
    public SessionCard sessionPrefab;
    public Transform sessionParent;
    public int noOfCards;

    [Button]
    public void PopulateEvents()
    {
        ClearCards();
        foreach (var data in EventListManager.Instance.eventsListPayload.data)
        {
            noOfCards++;
            var sessionCard = Instantiate(sessionPrefab, sessionParent);
            sessionCard.GetComponent<SessionCard>().FillData(data);
        }
    }

    private void ClearCards()
    {
        foreach (Transform child in sessionParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}