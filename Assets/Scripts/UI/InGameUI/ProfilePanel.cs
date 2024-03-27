using Doozy.Runtime.UIManager.Components;
using FF;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePanel : MonoBehaviour
{
    [Header ("Settings")]
    public UISlider environmentVolumeSlider;
    public AudioSource environmentAudioSource;

    [Header("About Player")]
    [SerializeField] private Image profilePictureOnTop;
    [SerializeField] private TextMeshProUGUI userNameOnTop;
    [SerializeField] private Image profilePicture;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI email;
    [SerializeField] private TextMeshProUGUI designation;
    [SerializeField] private TextMeshProUGUI about;

    private void Start()
    {
        SetPlayerInfo();
    }
    public void ChangeEnvironmentVolume()
    {
        environmentAudioSource.volume = environmentVolumeSlider.value;
    }

    public void SetPlayerInfo()
    {
        profilePictureOnTop.sprite = DataManager.Instance.userData.profileImage;
        userNameOnTop.text = DataManager.Instance.userData.name;
        profilePicture.sprite = DataManager.Instance.userData.profileImage;
        playerName.text = DataManager.Instance.userData.name;
        email.text = DataManager.Instance.userData.email;
        designation.text = DataManager.Instance.userData.designation;
        about.text = DataManager.Instance.userData.bio;
    }
        
}
