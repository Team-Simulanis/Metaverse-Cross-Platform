using System.Collections.Generic;
using System.Threading.Tasks;
using Doozy.Runtime.UIManager.Components;
using Simulanis;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using DebugManager = Simulanis.DebugManager;

// This class is responsible for managing cloud characters
public class CloudCharacterManager : MonoBehaviour
{
 
    [FormerlySerializedAs("malePrefab")] public GameObject avatarIconPrefab; // Prefab for the male character //
    public GameObject parent; // Prefab for the male character //
    [FormerlySerializedAs("cp")] public CharacterPayload characterPayload; // Payload for the character

    // URL for the avatar list
    private const string AvatarListURL = "https://api.simulanis.io/api/resource/3d/character/list/universal/";

    [DisplayAsString] public int avatarCount; // Count of avatars

    public static CloudCharacterManager Instance;

    private GameObject _obj;

    public UIButton startButton;
    public void Start()
    {
        Instance = this;
    }

    // Asynchronously get the cloud avatar data
    public async Task GetCloudAvatarData(string type)
    {
        avatarCount = 0; //will reset the list whenever a new gender is selected
        var result = await WebRequestManager.GetWebRequestWithAuthorization(AvatarListURL, type);

        characterPayload = JsonUtility.FromJson<CharacterPayload>(result);
        CleanUpAvatarList();
        // Instantiate necessary prefabs for character selection
        foreach (var t in characterPayload.data)
        {
            avatarCount++;
            _obj = Instantiate(avatarIconPrefab, parent.transform);
            avatarButtonList.Add(_obj);
            _obj.SetActive(true);
            _obj.AddComponent<CloudCharacterPrefabHolder>();
            _obj.GetComponent<CloudCharacterPrefabHolder>().characterData = t;
        }
    }

    private void CleanUpAvatarList()
    {
        DebugManager.Log(DebugType.Avatar,"Cleaning up avatar list");
        foreach (var avatar in avatarButtonList)
        {
            Destroy(avatar);
        }
        avatarButtonList.Clear();
    }
    
    public List<GameObject> avatarButtonList = new();
}