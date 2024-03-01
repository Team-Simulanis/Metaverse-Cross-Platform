using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class CloudCharacterManager : MonoBehaviour
{
    public GameObject malePrefab;
    [FormerlySerializedAs("CP")] public CharacterPayload cp;

    private const string AvatarListURL = "https://api.simulanis.io/api/resource/3d/character/list/universal/";

    private const string BearerToken =
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1dWlkIjoiOTJjNmM1ZGQtZGIxNy00ODMzLWFhYzEtNjNkNmE5MmJmZjg5IiwiY2hhbm5lbCI6ImVmMjFlMWUwLWI4MGUtNGQwMy04MGRiLTJjN2E0OThmNTJlYiIsImlhdCI6MTcwODYwNzc0MSwiZXhwIjoxNzExMTk5NzQxfQ.uNK1mBZT8OnY2V6fhLq2kMMnPBaLrDXx49u6ph8nDU4";

   [DisplayAsString]
   public int avatarCount;
    private void Start()
    {
        malePrefab.SetActive(false);
        GetCloudAvatarData("female");
    }

    private async void GetCloudAvatarData(string type)
    {
      var result = await WebRequestManager.WebRequestWithAuthorization(AvatarListURL,type, BearerToken);
      
      cp = JsonUtility.FromJson<CharacterPayload>(result);

      avatarCount=0;
      
      foreach (var t in cp.data) //instantiating necessary prefabs for characters selection
      {
          avatarCount++;
          var obj = Instantiate(malePrefab, malePrefab.transform.parent.transform);
          obj.SetActive(true);
          obj.AddComponent<CloudCharacterPrefabHolder>();
          obj.GetComponent<CloudCharacterPrefabHolder>().characterData = t;
      }
    }
}

