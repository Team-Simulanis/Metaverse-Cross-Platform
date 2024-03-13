using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

// This class is responsible for managing cloud characters
public class CloudCharacterManager : MonoBehaviour
{
    public GameObject malePrefab; // Prefab for the male character //
    [FormerlySerializedAs("CP")] public CharacterPayload cp; // Payload for the character

    // URL for the avatar list
    private const string AvatarListURL = "https://api.simulanis.io/api/resource/3d/character/list/universal/";
    
    [DisplayAsString] public int avatarCount; // Count of avatars

    public static CloudCharacterManager instance;
    GameObject obj;
    // On start, deactivate the male prefab and get the cloud avatar data
    private void Start()
    {
        instance = this;
        malePrefab.SetActive(false);
        GetCloudAvatarData("male");
    }

    // Asynchronously get the cloud avatar data
    public async void GetCloudAvatarData(string type)
    {
        avatarCount = 0; //will reset the list whenever a new gender is selected
        var result = await WebRequestManager.GetWebRequestWithAuthorization(AvatarListURL,type);

        cp = JsonUtility.FromJson<CharacterPayload>(result);
        
        // Instantiate necessary prefabs for character selection
        foreach (var t in cp.data)
        {
            avatarCount++;
            obj = Instantiate(malePrefab, malePrefab.transform.parent.transform);
            obj.SetActive(true);
            obj.AddComponent<CloudCharacterPrefabHolder>();
            obj.GetComponent<CloudCharacterPrefabHolder>().characterData = t;
        }
    }
}