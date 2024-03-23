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
}
