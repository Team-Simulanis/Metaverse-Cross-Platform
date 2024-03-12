using System;
using System.Collections;
using System.Collections.Generic;
using FF;
using TMPro;
using UnityEngine;
using Sirenix.OdinInspector;
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

    public UserDetailsPayload userDetailsPayload = new ();
    // public userInfo userInfo;

    public string bearerToken= "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1dWlkIjoiYWYzNTY0YzItNzgzNC00MzhmLWFmOWEtMjZiZWNkMjllM2RiIiwiY2hhbm5lbCI6ImVmMjFlMWUwLWI4MGUtNGQwMy04MGRiLTJjN2E0OThmNTJlYiIsImlhdCI6MTcwOTg5MDEwMCwiZXhwIjoxNzM1ODEwMTAwfQ.fN_78fl8zUgzXzhlPzpjjPbyBc7eWIths9jJRyjwlno";
    public string endPoint = "https://metaverse-backend.simulanis.io/api/application/user/details";

    private void Start()
    {
        GetUserProfileData();
    }

    public async void GetUserProfileData()
    {
        var result = await WebRequestManager.WebRequestWithAuthorization(endPoint, "", bearerToken);
        userDetailsPayload = JsonUtility.FromJson<UserDetailsPayload>(result);
        profilePicture.sprite = await WebRequestManager.DownloadSVG(userDetailsPayload.data.group.avatar);
        profilePicture.useSpriteMesh = true;
        profilePicture.color = Color.black;
    }
     public void takeInfo()
     {
    //   userInfo.name = nameInputField.text;
    //   userInfo.email = emailInputField.text;
    //   userInfo.designation = designationInputField.text;
    //   userInfo.experience = experienceInputField.text;
    //   userInfo.bio = bioInputField.text;
    
        DataManager.Instance.userData.designation = designationInputField.text;
        DataManager.Instance.userData.experience = experienceInputField.text;
        DataManager.Instance.userData.bio = bioInputField.text;
        
    }
}
