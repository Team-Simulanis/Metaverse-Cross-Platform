using UnityEngine;
using ReadyPlayerMe.Core;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public enum PlayerType
{
    Offline,
    Networked
}

public enum AvatarBodyType
{
    Masculine,
    Feminine
}

[RequireComponent(typeof(AvatarInitializer))]
public class RPMPlayerManager : MonoBehaviour
{
    public PlayerType playerType;
    public AvatarBodyType avatarBodyType;

    [Required] public GameObject defaultAvatar;
    [ReadOnly] public GameObject currentAvatar;

    private AvatarObjectLoader _avatarObjectLoader;

    private GameObject _avatarController;

    [ShowInInspector][ReadOnly] [Tooltip("RPM avatar URL or shortcode to load")]
    private string _avatarUrl;

    private readonly Vector3 _avatarPositionOffset = new Vector3(0, 0, 0);
    
    public string currentAvatarUrl;

    private AvatarInitializer _avatarInitializer;
    private AvatarNetworkManager _avatarNetworkManager;

    public GameObject mobileController;
    [FoldoutGroup("Events")] public UnityEvent onAvatarLoaded = new();

    private async void Start()
    {
        _avatarInitializer = GetComponent<AvatarInitializer>();
       
        if( GetComponent<AvatarNetworkManager>())
        {
            _avatarNetworkManager = GetComponent<AvatarNetworkManager>();
        }
        else
        {
            Debug.LogError("Player network manager Not Available");
        }

        //Initializing the default avatar
        currentAvatar = await _avatarInitializer.SetupAvatar(defaultAvatar, currentAvatar, avatarBodyType, _avatarPositionOffset);
    }
    
    public void StartLoadingAvatar(string url)
    {
        _avatarObjectLoader = new AvatarObjectLoader();
        _avatarObjectLoader.OnCompleted += OnLoadCompleted;
        _avatarObjectLoader.OnProgressChanged += OnLoading;
        _avatarObjectLoader.OnFailed += OnLoadFailed;
        onAvatarLoaded?.Invoke();
        LoadAvatar(url);
    }

    private static void OnLoading(object sender, ProgressChangeEventArgs e)
    {
        Debug.Log("Loading Avatar..." + e.Progress + "%");
    }

    private static void OnLoadFailed(object sender, FailureEventArgs args)
    {
        Debug.Log("Failed to load avatar");
    }

    private async void OnLoadCompleted(object sender, CompletionEventArgs args)
    {
        Debug.Log("Avatar Loaded :" + args.Metadata.OutfitGender);

        avatarBodyType = args.Metadata.OutfitGender switch
        {
            OutfitGender.Masculine => AvatarBodyType.Masculine,
            OutfitGender.Feminine => AvatarBodyType.Feminine,
            _ => avatarBodyType
        };

        if(_avatarNetworkManager==null)
        {
            currentAvatar = await _avatarInitializer.SetupAvatar(args.Avatar, currentAvatar, avatarBodyType, _avatarPositionOffset);
        }
        else if(_avatarNetworkManager.IsOwner)
        {
            Debug.Log("Initializing Controls for local player");
            currentAvatar = await _avatarInitializer.SetupAvatar(args.Avatar, currentAvatar, avatarBodyType, _avatarPositionOffset);
            Debug.Log("Initialized Controls for local player");
        }
        else
        {
            Debug.Log("Initializing Controls ignored as its a remote player");
        }

        _avatarInitializer.AnimatorRebind();
        
        if(playerType == PlayerType.Networked)

        {
            if (_avatarNetworkManager.IsOwner) //TODO: Refactor this to a only work for networked players
            {
#if UNITY_ANDROID
            mobileController.SetActive(true);
#endif
            }
        }
        else
        {
#if UNITY_ANDROID
            mobileController.SetActive(true);
#endif
        }
    }

    public void InitializeControls()
    {
        
    }

  

    public void LoadAvatar(string url)
    {
        _avatarUrl = url.Trim(' ');
        _avatarObjectLoader.LoadAvatar(_avatarUrl);
    }

    [Button]
    public void ChangeAvatarUrl()
    {
        if(_avatarNetworkManager==null)
        {
            _avatarNetworkManager = GetComponent<AvatarNetworkManager>();
        }
        else
        {
            Debug.Log("Available");
        }
        _avatarNetworkManager.SendAvatarUpdateRequestServer(currentAvatarUrl);
    }
}

//TODO: Refactor this complete script as currently this is mixes with networking and avatar loading