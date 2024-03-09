using Simulanis.Player;
using UnityEngine;

public class LocationsPanel : MonoBehaviour //
{
    public static LocationsPanel instance;
    public GameObject ownerObject;
    private void Start()
    {
        instance = this;
    }
    public void MoveToThisLocation(Transform Location)
    {
        ownerObject.transform.position = Location.position;
    }
    public void TakeOwner(GameObject OwnerObject)
    {
        ownerObject = OwnerObject;
    }
}
