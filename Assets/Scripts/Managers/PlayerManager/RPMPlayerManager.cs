using UnityEngine;
#if !UNITY_Server
using ReadyPlayerMe.Core;
#endif
using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using FF;
using FishNet.Object;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(AvatarInitializer))]
public class RPMPlayerManager : NetworkBehaviour
{
    public GameObject defaultAvatar;

    public delegate void UrlChanger(string url);

    public static UrlChanger urlChanger;
    public event Action OnLoadComplete;
    private AvatarObjectLoader avatarObjectLoader;
    [FormerlySerializedAs("avatar")] public GameObject currentAvatar;
    private Animator animator = null;
    private GameObject avatarController;
    public bool isMale;

    [SerializeField] [Tooltip("RPM avatar URL or shortcode to load")]
    private string avatarUrl;

    public GameObject invectorControl;
    private readonly Vector3 avatarPositionOffset = new Vector3(0, 0, 0);
    public bool changeAvatar;
    public bool InitAgain;

    [Tooltip("This will be true for Network Object")]
    public bool isNetworkObject;

    public UnityEvent onAvatarLoaded = new(); 

    public AvatarInitializer avatarInitializer;
    public GameObject mobileController;

    private async void Start()
    {
        avatarInitializer = GetComponent<AvatarInitializer>();
        currentAvatar = await avatarInitializer.SetupAvatar(defaultAvatar, currentAvatar,
            isNetworkObject, isMale, invectorControl, avatarPositionOffset);
    }

    [Button]
    public void AnimatorRebind()
    {
        invectorControl.GetComponent<Animator>().Rebind();
        invectorControl.GetComponent<Animator>().Update(0f);
    }

    // Start is called before the first frame update
    public override void OnStartClient()
    {
        base.OnStartClient();

        if (IsOwner)
        {
            if (DataManager.Instance != null)
                avatarUrl = DataManager.Instance.userData.avatarDetails.avatarModelDownloadLink;
            isNetworkObject = false;
        }
        else
        {
            isNetworkObject = true;
        }

        if (isBufferAvailable)
        {
            Debug.Log("Already Buffered Avatar Available");
            return;
        }

        newUrl = avatarUrl;
        ChangeAvatarUrl();
    }

    public void LoadAvatar()
    {
        avatarObjectLoader = new AvatarObjectLoader();
        avatarObjectLoader.OnCompleted += OnLoadCompleted;
        avatarObjectLoader.OnProgressChanged += OnLoading;
        avatarObjectLoader.OnFailed += OnLoadFailed;
        onAvatarLoaded?.Invoke();
        LoadAvatar(avatarUrl);
    }

    public void SetAvatarUrl(string value)
    {
        avatarUrl = value;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeAvatar)
        {
            LoadAvatar();
            changeAvatar = false;
        }

        if (InitAgain)
        {
            InitAgain = false;
        }
    }

    private void OnLoading(object sender, ProgressChangeEventArgs e)
    {
        Debug.Log("Loading Avatar..." + e.Progress + "%");
    }

    private void OnLoadFailed(object sender, FailureEventArgs args)
    {
        Debug.Log("Failed to load avatar");
    }

    private async void OnLoadCompleted(object sender, CompletionEventArgs args)
    {
        Debug.Log("Avatar Loaded :" + args.Metadata.OutfitGender);
        isMale = args.Metadata.OutfitGender == OutfitGender.Masculine;
        currentAvatar = await avatarInitializer.SetupAvatar(args.Avatar, currentAvatar,
        isNetworkObject, isMale,
        invectorControl, avatarPositionOffset);

        await UniTask.DelayFrame(1);

        AnimatorRebind();

        if(IsOwner)
        {
            //StickyNotesManager._instance.AssignPlayerTransform(this.transform);
#if UNITY_ANDROID
            mobileController.SetActive(true);
#endif
        }
    }

    public void LoadAvatar(string url)
    {
        avatarUrl = url.Trim(' ');
        avatarObjectLoader.LoadAvatar(avatarUrl);
    }

    public void changeUrl(string url)
    {
        avatarUrl = "https://models.readyplayer.me/" + url + ".glb";
    }

    /// <summary>
    /// Use this function to change url
    /// </summary>
    /// <param name="url">Pass .GLB url here</param>
    public string newUrl;

    [Button]
    public void ChangeAvatarUrl()
    {
        avatarUrl = newUrl;
        ChangePlayerAvatarServer(gameObject, newUrl);
    }

    [ServerRpc]
    private void ChangePlayerAvatarServer(GameObject manager, string url)
    {
        Debug.Log("Server Received");
        ChangePlayerAvatar(manager, url);
    }

    public bool isBufferAvailable;

    [ObserversRpc(BufferLast = true, ExcludeOwner = false, RunLocally = true)]
    private void ChangePlayerAvatar(GameObject manager, string url)
    {
        Debug.Log("Server Changed");
        isBufferAvailable = true;
        Debug.Log(url);
        //avatarUrl = "https://cdn.simulanis.io/sso/uno/production/resources/fde87370-4243-43dc-9978-846e1511fed4/3DAssets/universal/1/M11.glb";
        avatarUrl = url;
        LoadAvatar();
    }
}

//TODO: Refactor this complete script as currently this is mixes with networking and avatar loading