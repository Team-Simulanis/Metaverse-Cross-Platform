using UnityEngine;
using UnityEngine.UI;
using FF;
using ReadyPlayerMe.Samples.QuickStart;

// This class is responsible for holding the data of a cloud character prefab
public class CloudCharacterPrefabHolder : MonoBehaviour
{
    public CharacterData characterData; // Data of the character
    public Image image; // Image component to display the character's thumbnail

    // On start, initialize the image component, set up the button click listener, and get the character's texture
    private void Start()
    {
        image = transform.GetChild(1).GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(ButtonClick);
        GetTexture();
    }

    // When the button is clicked, load the avatar and update the avatar info
    private void ButtonClick()
    {
        ThirdPersonLoader.instance.LoadAvatar(characterData.assets[0].url);
        DataManager.Instance.UpdateAvatarInfo(new AvatarDetails
        {
            avatarModelDownloadLink = characterData.assets[0].url
        });
    }

    // Asynchronously get the character's texture from the thumbnail URL
    private async void GetTexture()
    {
        var sprite = await WebRequestManager.ImageDownloadRequest(characterData.thumbnail+"?camera=portrait");
        SetImage(sprite);
    }

    // Set the sprite of the Image component and adjust its properties
    private void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
        image.preserveAspect = true;
        image.color = Color.white;
    }
}