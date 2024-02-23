using UnityEngine;
#if !UNITY_SERVER
using ReadyPlayerMe.Core;
#endif
using System;
using FF;
using FishNet.Connection;
using FishNet.Object;
using Invector.vCharacterController;
using Sirenix.OdinInspector;


public class RPMPlayerManager : NetworkBehaviour
{
    private string maleType = "Masculine";
    public Animator femaleAnimator;
    public Animator maleAnimator;
    public GameObject defaultAvatar;
    public delegate void UrlChanger(string url);

    public static UrlChanger urlChanger;
    public event Action OnLoadComplete;
#if !UNITY_SERVER
    private AvatarObjectLoader avatarObjectLoader;
#endif
    private GameObject avatar;
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

    private void Start()
    {
        SetupAvatar(defaultAvatar);
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
        if(isBufferAvailable)
        {
            Debug.Log("Already Buffered Avatar Available");
            return;
        }

        newUrl = avatarUrl;
        ChangeAvatarUrl();
    }


    public void LoadAvatar()
    {
#if !UNITY_SERVER
        avatarObjectLoader = new AvatarObjectLoader();
        avatarObjectLoader.OnCompleted += OnLoadCompleted;
        avatarObjectLoader.OnProgressChanged += OnLoading;
        avatarObjectLoader.OnFailed += OnLoadFailed;
        LoadAvatar(avatarUrl);
#endif
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
            InitController();
            InitAgain = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (IsOwner)
                ChangeAvatarUrl();
        }
    }

    void InitController()
    {
        // avatarController
    }
#if !UNITY_SERVER
    private void OnLoading(object sender, ProgressChangeEventArgs e)
    {
        Debug.Log("Loading Avatar..." + e.Progress + "%");
    }

    private void OnLoadFailed(object sender, FailureEventArgs args)
    {
        Debug.Log("Failed to load avatar");
    }

    private void OnLoadCompleted(object sender, CompletionEventArgs args)
    {
        SetupAvatar(args.Avatar);
        Debug.Log("Avatar Loaded :" + args.Metadata.OutfitGender);
        if (args.Metadata.OutfitGender == OutfitGender.Masculine)
        {
            isMale = true;
        }
        else
        {
            isMale = false;
        }
    }
#endif

    private void SetupAvatar(GameObject targetAvatar)
    {
        Debug.Log("Setting up "+ targetAvatar.name + " as Avatar");
        if (avatar != null)
        {
            avatarController.GetComponent<vThirdPersonController>().enabled = false;
            avatarController.GetComponent<vThirdPersonInput>().enabled = false;
            Destroy(avatar);
        }
        else
        {
            avatarController = invectorControl;
            avatarController.SetActive(true);
            animator = avatarController.GetComponent<Animator>();
        }

        avatar = targetAvatar;
        avatar.transform.parent = avatarController.transform.GetChild(0);
        avatar.transform.localPosition = avatarPositionOffset;
        avatar.transform.localRotation = Quaternion.Euler(0, 0, 0);
        DestroyImmediate(avatar.GetComponent<Animator>());
        if (!isNetworkObject)
        {
            avatarController.GetComponent<vThirdPersonController>().enabled = true;
            avatarController.GetComponent<vThirdPersonInput>().enabled = true;
            avatarController.transform.GetChild(3).gameObject.SetActive(true);
        }

        avatarController.SetActive(true);
        Invoke(nameof(ChangeAvatarRef), 0.1f);
    }

    private void ChangeAvatarRef()
    {
        if (isMale)
        {
            animator.avatar = femaleAnimator.avatar;
            animator.avatar = maleAnimator.avatar;
        }
        else
        {
            animator.avatar = maleAnimator.avatar;
            animator.avatar = femaleAnimator.avatar;
        }
    }

    public void LoadAvatar(string url)
    {
        avatarUrl = url.Trim(' ');
#if !UNITY_SERVER       
        avatarObjectLoader.LoadAvatar(avatarUrl);
#endif
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
        avatarUrl = url;
        LoadAvatar();
    }
}