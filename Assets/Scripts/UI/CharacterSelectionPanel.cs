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
    
    public void ShowDetails()
    {
        userName.text = DataManager.Instance.userData.name;
        profileImage.sprite = DataManager.Instance.userData.profileImage;
    }
}
