
using UnityEngine;
using ReadyPlayerMe.Core;
using System;
using FishNet.Object;
using Invector.vCharacterController;
using Sirenix.OdinInspector;

public class RPMManager_FF : NetworkBehaviour
{
    private string maleType = "Masculine";
    public Animator femaleAnimator;
    public Animator maleAnimator;
    public delegate void UrlChanger(string url);
    public static  UrlChanger urlChanger;
    public event Action OnLoadComplete;
    private AvatarObjectLoader avatarObjectLoader;
    public GameObject avatar;
    private Animator animator =  null;
    public bool isMale;
    [SerializeField][Tooltip("RPM avatar URL or shortcode to load")] 
    private string avatarUrl;
    
   
    private readonly Vector3 avatarPositionOffset = new Vector3(0, 0, 0);

    [ReadOnly] public bool avatarLoadingInProgressCompleted = true;
   [ShowIf("avatarLoadingInProgressCompleted")] public bool changeAvatar;
    public bool InitAgain;

    public GameObject defaultAvatar;
    // Start is called before the first frame update
    void Start()
    {
      
    }
    public void LoadAvatar()
    {
        avatarLoadingInProgressCompleted = false;
        avatarObjectLoader = new AvatarObjectLoader();
        avatarObjectLoader.OnCompleted += OnLoadCompleted;
        avatarObjectLoader.OnProgressChanged += _OnLoading;
        avatarObjectLoader.OnFailed += OnLoadFailed;
        LoadAvatar(avatarUrl);
    }

    public void SetAvatarUrl(string value)
    {
        avatarUrl = value;
    }
    // Update is called once per frame
    void Update()
    {
       
        GetComponent<vThirdPersonInput>().enabled = IsOwner;
        if(changeAvatar)
        {
            LoadAvatar();
            Debug.Log("Trying To Load Avatar");
            changeAvatar = false;
        }
        if(InitAgain)
        {
            InitController();
            InitAgain = false;
        }
    }
    void InitController()
    {
        // avatarController
    }
    private void OnLoading(object sender, ProgressChangeEventArgs e)
    {
        avatarLoadingInProgressCompleted = false;
        throw new NotImplementedException();
    }

    private void OnLoadFailed(object sender, FailureEventArgs args)
    {
        avatarLoadingInProgressCompleted = true;
    }
    private void _OnLoading(object sender, ProgressChangeEventArgs args)
    {
        
    }
    private void OnLoadCompleted(object sender, CompletionEventArgs args)
    {
        avatarLoadingInProgressCompleted = true;
        SetupAvatar(args.Avatar);
        Debug.Log(args.Metadata.OutfitGender);
        if(args.Metadata.OutfitGender == OutfitGender.Masculine)
        {
            isMale = true;
        }
        else
        {
            isMale = false;
        }
    }

    public bool firstInitializing;
    private void SetupAvatar(GameObject  targetAvatar)
    {
       
        if (avatar != null)
        {
            GetComponent<vThirdPersonController>().enabled = false;
            GetComponent<vThirdPersonInput>().enabled = false;
            Destroy(avatar);
        }
        else
        {
            firstInitializing = true;
            animator = GetComponent<Animator>();
        }
        avatar = targetAvatar;
        avatar.transform.parent = transform.GetChild(0);
        avatar.transform.localPosition = avatarPositionOffset;
        avatar.transform.localRotation = Quaternion.Euler(0, 0, 0);
        DestroyImmediate(avatar.GetComponent<Animator>());
      
  
            GetComponent<vThirdPersonController>().enabled = true;
            GetComponent<vThirdPersonInput>().enabled = true;
  
        avatarLoadingInProgressCompleted = true;
        //avatarController.SetActive(true);
        //Debug.Log(avatar.GetComponent<AvatarData>().avatarMetadata.OutfitGender);
        Invoke("ChangeAvatarRef", 0.1f);
        
    }

    public override void OnStartClient()
    {
        
        base.OnStartClient();
        if(IsOwner)
        {
            SetupAvatar(defaultAvatar);
        }
    }

    void ChangeAvatarRef()
    {
        if(isMale)
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
    // private void SetupAvatar(GameObject  targetAvatar)
    // {
    //     Animator animator;
    //     if (avatar != null)
    //     {
    //         Destroy(avatar);
    //         Destroy(avatarController);
    //     }
    //     avatar = targetAvatar;
    //     avatarController = Instantiate(invectorControl);
    //     avatar.transform.parent = avatarController.transform.GetChild(0);
    //     avatar.transform.localPosition = avatarPositionOffset;
    //     avatar.transform.localRotation = Quaternion.Euler(0, 0, 0);
    //     animator = avatarController.GetComponent<Animator>();
    //     animator.avatar = avatar.GetComponent<Animator>().avatar;
    //     DestroyImmediate(avatar.GetComponent<Animator>());
    //     avatarController.SetActive(true);
    // }
    public void LoadAvatar(string url)
    {
        //remove any leading or trailing spaces
        avatarUrl = url.Trim(' ');
        avatarObjectLoader.LoadAvatar(avatarUrl);
    }
   
    public void changeUrl(string url)
    {
        avatarUrl = "https://models.readyplayer.me/"+url+".glb";
        
    }
}
