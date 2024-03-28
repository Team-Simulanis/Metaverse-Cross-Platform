
using System.Diagnostics;
using System.Threading.Tasks;
using Doozy.Runtime.Signals;

using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [ReadOnly] public AccessData accessData;

    public Image loginLogo;

    public TMP_Text loadingTextStatus;

    public int delayBetweenCalls = 1000;
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
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
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

        await Task.Delay(delayBetweenCalls);


        var e = EventListManager.Instance.FetchEvent();

        while (e.IsCompleted == false)
        {
            Debug.Log("Fetching Available Events");
            loadingTextStatus.text = "Fetching Available Events";
            await Task.Delay(100);
        }

        await Task.Delay(delayBetweenCalls);

        var t = UIManager.Instance.infoPanel.GetComponent<InfoPanel>().GetUserProfileData();
        while (t.IsCompleted == false)
        {
            Debug.Log("Fetching User Data");
            loadingTextStatus.text = "Fetching User Data";
            await Task.Delay(100);
        }
        await Task.Delay(delayBetweenCalls);
        
        await AddressableManager.Instance.StartAddressable();
        
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
        
        
        stopwatch.Stop();
        Debug.Log("Total Time Taken: " + stopwatch.ElapsedMilliseconds/1000 + " Seconds");
        
        Signal.Send(StreamId.Screens.InforScreen);
    }

    private async Task UpdateBrandingDetails()
    {
        loginLogo.sprite =
            await WebRequestManager.ImageDownloadRequest(
                "https://drive.google.com/uc?export=download&id=1irLG5jAF6w3qJG4Lota4cnWFiTJoDbQO");
    }
}