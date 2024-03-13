using FF;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// [System.Serializable]
// public struct userInfo{
//     [ReadOnly] public string name;
//     [ReadOnly]public string email;
//     [ReadOnly]public string designation;   
//     [ReadOnly]public string experience;
//     [ReadOnly]public string bio;   

// }
public class InfoPanel : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] TMP_InputField emailInputField;
    [SerializeField] TMP_InputField designationInputField;
    [SerializeField] TMP_InputField experienceInputField;
    [SerializeField] TMP_InputField bioInputField;
    [SerializeField] private Image profilePicture;

    public UserDetailsPayload userDetailsPayload = new();
    // public userInfo userInfo;

 
    public string endPoint = "https://metaverse-backend.simulanis.io/api/application/user/details";

    private void Start()
    {
        GetUserProfileData();
    }

    private async void GetUserProfileData()
    {
        var result = await WebRequestManager.GetWebRequestWithAuthorization(endPoint, "");
        userDetailsPayload = JsonUtility.FromJson<UserDetailsPayload>(result);
        profilePicture.sprite = await WebRequestManager.DownloadSvg(userDetailsPayload.data.group.avatar);
        profilePicture.useSpriteMesh = true;
        profilePicture.color = Color.black;
        DataManager.Instance.userData.profileImage = profilePicture.sprite;
        DataManager.Instance.userData.designation = userDetailsPayload.data.designation;
        DataManager.Instance.userData.email = userDetailsPayload.data.email;
        DataManager.Instance.userData.name = userDetailsPayload.data.name;
        DataManager.Instance.userData.bio = userDetailsPayload.data.bio;
        ShowData();
        //DataManager.Instance.userData.experience = userDetailsPayload.data.experience;
    }

    private void ShowData()
    {
        nameInputField.text = DataManager.Instance.userData.name;
        emailInputField.text = DataManager.Instance.userData.email;
        designationInputField.text = DataManager.Instance.userData.designation;
        bioInputField.text = DataManager.Instance.userData.bio;
    }

    public void TakeInfo()
    {
        DataManager.Instance.userData.designation = designationInputField.text;
        DataManager.Instance.userData.experience = experienceInputField.text;
        DataManager.Instance.userData.bio = bioInputField.text;
    }
}