using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CloudCharacterManager : MonoBehaviour
{
    public GameObject malePrefab;
    public CharacterPayload CP;
    // Start is called before the first frame update
    void Start()
    {
        malePrefab.SetActive(false);
        StartCoroutine(GetCloudMaleData());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator GetCloudMaleData() {
        UnityWebRequest www = UnityWebRequest.Get("https://api.simulanis.io/api/resource/3d/character/list/universal/male");
        www.SetRequestHeader("Authorization", "Bearer " + "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1dWlkIjoiOTJjNmM1ZGQtZGIxNy00ODMzLWFhYzEtNjNkNmE5MmJmZjg5IiwiY2hhbm5lbCI6ImVmMjFlMWUwLWI4MGUtNGQwMy04MGRiLTJjN2E0OThmNTJlYiIsImlhdCI6MTcwODYwNzc0MSwiZXhwIjoxNzExMTk5NzQxfQ.uNK1mBZT8OnY2V6fhLq2kMMnPBaLrDXx49u6ph8nDU4");
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            Debug.Log(www.downloadHandler.text);
            CP = JsonUtility.FromJson<CharacterPayload>(www.downloadHandler.text);
            for (int i = 0; i < CP.data.Length; i++)
            {
                var Obj = Instantiate(malePrefab, malePrefab.transform.parent.transform);
                Obj.SetActive(true);
                Obj.AddComponent<CloudCharacterPrefabHolder>();
                Obj.GetComponent<CloudCharacterPrefabHolder>().characterData = CP.data[i];
            }
        }
    }
}
[System.Serializable]
public class CharacterPayload
{
    public bool status;
    public CharacterData[] data; 
}
[System.Serializable]
public class CharacterData
{
    public string name;
    public string thumbnail;
    public CharacterDataAssets[] assets;
}
[System.Serializable]
public class CharacterDataAssets
{
    public string url;
    public string type;
}
