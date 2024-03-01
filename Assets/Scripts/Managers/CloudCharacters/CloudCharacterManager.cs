using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

// This class is responsible for managing cloud characters
public class CloudCharacterManager : MonoBehaviour
{
    public GameObject malePrefab; // Prefab for the male character
    [FormerlySerializedAs("CP")] public CharacterPayload cp; // Payload for the character

    // URL for the avatar list
    private const string AvatarListURL = "https://api.simulanis.io/api/resource/3d/character/list/universal/";

    // Bearer token for authorization
    private const string BearerToken =
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1dWlkIjoiOTJjNmM1ZGQtZGIxNy00ODMzLWFhYzEtNjNkNmE5MmJmZjg5IiwiY2hhbm5lbCI6ImVmMjFlMWUwLWI4MGUtNGQwMy04MGRiLTJjN2E0OThmNTJlYiIsImlhdCI6MTcwODYwNzc0MSwiZXhwIjoxNzExMTk5NzQxfQ.uNK1mBZT8OnY2V6fhLq2kMMnPBaLrDXx49u6ph8nDU4";

    [DisplayAsString] public int avatarCount; // Count of avatars

    // On start, deactivate the male prefab and get the cloud avatar data
    private void Start()
    {
        malePrefab.SetActive(false);
        GetCloudAvatarData("female");
    }

    // Asynchronously get the cloud avatar data
    private async void GetCloudAvatarData(string type)
    {
        var result = await WebRequestManager.WebRequestWithAuthorization(AvatarListURL,type, BearerToken);

        cp = JsonUtility.FromJson<CharacterPayload>(result);
        
        // Instantiate necessary prefabs for character selection
        foreach (var t in cp.data)
        {
            avatarCount++;
            var obj = Instantiate(malePrefab, malePrefab.transform.parent.transform);
            obj.SetActive(true);
            obj.AddComponent<CloudCharacterPrefabHolder>();
            obj.GetComponent<CloudCharacterPrefabHolder>().characterData = t;
        }
    }
}