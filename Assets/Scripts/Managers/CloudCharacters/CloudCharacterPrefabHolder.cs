using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using FF;

public class CloudCharacterPrefabHolder : MonoBehaviour
{
    public CharacterData characterData;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ButtonClick);
        StartCoroutine(GetTexture());
    }
    void ButtonClick()
    {
        DataManager.Instance.UpdateAvatarInfo(new AvatarDetails()
            {
                avatarModelDownloadLink = characterData.assets[0].url
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator GetTexture() 
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(characterData.thumbnail);
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            SetImage(myTexture);
        }
    }
    private void SetImage(Texture2D texture)
    {
        Image image = transform.GetChild(1).GetComponent<Image>();
        var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
        image.sprite = sprite;
        image.preserveAspect = true;
        image.color = Color.white;
    }
}