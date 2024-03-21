using System;
using System.Threading.Tasks;
using Doozy.Runtime.Signals;
using Doozy.Runtime.UIManager.Components;
using FF;
using MPUIKIT;
using ReadyPlayerMe.Core;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [ReadOnly] public AccessData accessData;

    public Image loginLogo;

    public TMP_Text loadingTextStatus;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitApplication();
    }

    private async void InitApplication()
    {
        var b = UpdateBrandingDetails();
        while (b.IsCompleted == false)
        {
            Debug.Log("Fetching Branding Details");
            loadingTextStatus.text = "Fetching Branding Details";
            await Task.Delay(100);
        }

        var c = GetComponentInChildren<CloudCharacterManager>().GetCloudAvatarData("male");

        while (c.IsCompleted == false)
        {
            Debug.Log("Fetching Cloud Avatar Data");
            loadingTextStatus.text = "Fetching Cloud Avatar Data";
            await Task.Delay(100);
        }

        await Task.Delay(3000);

        var t = UIManager.Instance.infoPanel.GetComponent<InfoPanel>().GetUserProfileData();
        while (t.IsCompleted == false)
        {
            Debug.Log("Fetching User Data");
            loadingTextStatus.text = "Fetching User Data";
            await Task.Delay(100);
        }
        await Task.Delay(3000);
        
        var a = AddressableManager.Instance.StartAddressable();
        
        while (AddressableManager.Instance.isAddressableInitialized == false)
        {
            Debug.Log("Initializing Addressable");
            loadingTextStatus.text = "Initializing Addressable";
            await Task.Delay(100);
        }
   
        while (AddressableManager.Instance.isEnvironmentLoaded == false)
        {
            Debug.Log("Loading Environment");
            loadingTextStatus.text = "Loading Environment";
            await Task.Delay(100);
        }
        
        
        Signal.Send(StreamId.Screens.InforScreen);
    }

    private async Task UpdateBrandingDetails()
    {
        loginLogo.sprite =
            await WebRequestManager.ImageDownloadRequest(
                "https://drive.google.com/uc?export=download&id=1irLG5jAF6w3qJG4Lota4cnWFiTJoDbQO");
    }
}