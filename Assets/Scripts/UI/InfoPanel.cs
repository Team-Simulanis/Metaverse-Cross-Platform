using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Sirenix.OdinInspector;

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

    // public userInfo userInfo;


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
