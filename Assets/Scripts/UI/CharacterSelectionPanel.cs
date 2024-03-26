using System.Collections;
using System.Collections.Generic;
using FF;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI userName;
    [SerializeField] private Image profileImage;
    [SerializeField] private RawImage profileImageRaw;

    private void Start()
    {
        DefaultMaleCharacter();
    }
    public void ShowDetails()
    {
        userName.text = DataManager.Instance.userData.name;
        if(ProfilePictureManager.Instance.profilePictureFormat == ProfilePictureManager.ProfilePictureFormat.Sprite)
        {
            profileImage.sprite = ProfilePictureManager.Instance.GetProfilePicture();
            profileImageRaw.gameObject.SetActive(false);
        }
        else
        {
            ProfilePictureManager.Instance.LoadSprite(profileImageRaw);
        }
    }

    public void DefaultMaleCharacter()
    {
        DataManager.Instance.userData.avatarDetails.avatarModelDownloadLink = "https://cdn.simulanis.io/sso/uno/production/resources/9378a98f-e63d-4c20-8dce-180806c1def9/3DAssets/universal/2/M2.glb";
    }
}
