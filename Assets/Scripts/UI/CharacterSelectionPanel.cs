using System.Collections;
using System.Collections.Generic;
using FF;
using TMPro;
using UnityEngine;

public class CharacterSelectionPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI userName;

    private void Start()
    {
        ShowUserName();
    }

    private void ShowUserName()
    {
        userName.text = DataManager.Instance.userData.name;
    }
}
