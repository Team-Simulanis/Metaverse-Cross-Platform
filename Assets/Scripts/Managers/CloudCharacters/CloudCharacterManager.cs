using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

// This class is responsible for managing cloud characters
public class CloudCharacterManager : MonoBehaviour
{
    public GameObject malePrefab; // Prefab for the male character //
    public CharacterPayload cp; // Payload for the character

    // URL for the avatar list
    private const string AvatarListURL = "https://api.simulanis.io/api/resource/3d/character/list/universal/";

    [DisplayAsString] public int avatarCount; // Count of avatars

    public static CloudCharacterManager Instance;

    GameObject obj;

    // On start, deactivate the male prefab and get the cloud avatar data
    private void Start()
    {
        Instance = this;
        malePrefab.SetActive(false);
        GetCloudAvatarData("male");
    }

    // Asynchronously get the cloud avatar data
    public async void GetCloudAvatarData(string type)
    {
        avatarCount = 0; //will reset the list whenever a new gender is selected
        var result = await WebRequestManager.GetWebRequestWithAuthorization(AvatarListURL, type);

        cp = JsonUtility.FromJson<CharacterPayload>(result);
        CleanUpAvatarList();
        // Instantiate necessary prefabs for character selection
        foreach (var t in cp.data)
        {
            avatarCount++;
            obj = Instantiate(malePrefab, malePrefab.transform.parent.transform);
            avatarButtonList.Add(obj);
            obj.SetActive(true);
            obj.AddComponent<CloudCharacterPrefabHolder>();
            obj.GetComponent<CloudCharacterPrefabHolder>().characterData = t;
        }
    }

    private void CleanUpAvatarList()
    {
        foreach (var avatar in avatarButtonList)
        {
            Destroy(avatar);
        }
        avatarButtonList.Clear();
    }
    
    public List<GameObject> avatarButtonList = new();
}