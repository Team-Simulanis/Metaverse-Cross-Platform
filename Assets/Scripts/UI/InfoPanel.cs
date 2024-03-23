using System;       
using System.Threading.Tasks;
using FF;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : Panel
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField designationInputField;
    [SerializeField] private TMP_InputField bioInputField;
    [SerializeField] private Image profilePicture;
    [SerializeField] private RawImage rawImageProfilePicture;

    public UserDetailsPayload userDetailsPayload = new();

    public string endPoint = "https://metaverse-backend.simulanis.io/api/application/user/details";
    public string[] exe;

    public async Task GetUserProfileData()
    {
        var result = await WebRequestManager.GetWebRequestWithAuthorization(endPoint, "");
        Debug.Log("Received Data: " + result);
        userDetailsPayload = JsonUtility.FromJson<UserDetailsPayload>(result);
        
        ProfilePictureManager.Instance.SetProfilePicture(userDetailsPayload.data.avatar,profilePicture, rawImageProfilePicture);

        profilePicture.useSpriteMesh = true;
        profilePicture.color = Color.black;
        DataManager.Instance.userData.profileImage = profilePicture.sprite;
        DataManager.Instance.userData.profileImageUrl = userDetailsPayload.data.avatar;
        DataManager.Instance.userData.designation = userDetailsPayload.data.designation;
        DataManager.Instance.userData.email = userDetailsPayload.data.email;
        DataManager.Instance.userData.name = userDetailsPayload.data.name;
        DataManager.Instance.userData.bio = userDetailsPayload.data.bio;
        ShowData();
    }


    private void ShowData()
    {
        nameInputField.text = DataManager.Instance.userData.name;
        emailInputField.text = DataManager.Instance.userData.email;
        designationInputField.text = DataManager.Instance.userData.designation;
        bioInputField.text = DataManager.Instance.userData.bio;
    }

    public async void UpdatePlayerInfo()
    {
        DataManager.Instance.userData.designation = designationInputField.text;
        DataManager.Instance.userData.bio = bioInputField.text;

        var payload = new UserDetailsPostPayload
        {
            name = nameInputField.text,
            designation = designationInputField.text,
            bio = bioInputField.text
        };


        var json = JsonUtility.ToJson(payload, true);

        await WebRequestManager.PostWebRequestWithAuthorization(
            "https://metaverse-backend.simulanis.io/api/application/user/update/details", json);
    }
}

[Serializable]
public class UserDetailsPostPayload
{
    public string name;
    public string email;
    public string designation;
    public string bio;
}