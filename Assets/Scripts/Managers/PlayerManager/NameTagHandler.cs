using FF;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameTagHandler : MonoBehaviour
{
    public TextMeshProUGUI inGameUsername;
    public string playerName;
    public bool isUpdated;
    public Image profileImage;
    public RawImage profileImageRaw;


    private void Start()
    {
        if (transform.parent.GetComponent<RPMPlayerManager>().playerType == PlayerType.Networked)
        {
            if (!transform.parent.GetComponent<AvatarNetworkManager>().IsOwner) return;
        }

        transform.parent.GetComponent<RPMPlayerManager>().onAvatarLoaded.AddListener(SetUserName);
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.accessData.testType != AccessData.Test.NameTag)
            {
                gameObject.SetActive(false);
                return;
            }
        }

        if (transform.parent.GetComponent<RPMPlayerManager>().playerType == PlayerType.Offline)
        {
            if (GameManager.Instance.accessData.testType != AccessData.Test.NameTag) return;
        }

        inGameUsername.text = $"Test+{RandomIDGenerator.GenerateRandomID()}";
        if (ProfilePictureManager.Instance)
            StartCoroutine(ProfilePictureManager.Instance.LoadGif(Application.persistentDataPath + "/profilePic.gif",
                profileImageRaw));
    }

    public void SetUsernameOnHost(string username, string profileImageURL)
    {
        isUpdated = true;
        inGameUsername.text = username;
        playerName = username;
        profileImageRaw.gameObject.SetActive(true);
        profileImage.gameObject.SetActive(false);

        StartCoroutine(ProfilePictureManager.Instance.LoadGif(profileImageURL, profileImageRaw));
    }


    private void SetUserName() //will be called on start to set the username of the player
    {
        if (!transform.parent.GetComponent<AvatarNetworkManager>()) return;
        var am = transform.parent.GetComponent<AvatarNetworkManager>();

        if (!am.IsOwner) return;

        Debug.Log("Am Owner");

        // am.SetUsernameOnServer(DataManager.Instance
        //     ? DataManager.Instance.userData.name
        //     : $"Player+{RandomIDGenerator.GenerateRandomID()}", DataManager.Instance.userData.profileImageUrl);

        inGameUsername.transform.parent.gameObject.SetActive(false);
    }
}