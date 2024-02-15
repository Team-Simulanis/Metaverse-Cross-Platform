using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadyPlayerMe.Core;
using System;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet;
using FishNet.Transporting;
using Invector.vCharacterController;
using ReadyPlayerMe.AvatarCreator;

public class RPMPlayerManager : NetworkBehaviour
{
    private string maleType = "Masculine";
    public Animator femaleAnimator;
    public Animator maleAnimator;
    public delegate void UrlChanger(string url);
    public static  UrlChanger urlChanger;
    public event Action OnLoadComplete;
    private AvatarObjectLoader avatarObjectLoader;
    private GameObject avatar;
    private Animator animator =  null;
    private GameObject avatarController;
    public bool isMale;
    [SerializeField][Tooltip("RPM avatar URL or shortcode to load")] 
    private string avatarUrl;
    
    public GameObject invectorControl;
    private readonly Vector3 avatarPositionOffset = new Vector3(0, 0, 0);
    public bool changeAvatar;
    public bool InitAgain;
    [Tooltip("This will be true for Network Object")]
    public bool isNetworkObject;
    [SyncVar(Channel = Channel.Unreliable, ReadPermissions = ReadPermission.Observers, OnChange = nameof(ChangePlayerAvatar))]
    public string Name =  null;
    public bool ChangeURLL;

    private void ChangePlayerAvatar(string prevUrl, string newUrl, bool asServer)
    {
        if(!asServer)
        {
            Debug.Log(prevUrl+" "+ newUrl);
            LoadAvatar(newUrl);
        }
    }
    // Start is called before the first frame update
    public override void OnStartClient()
    {
        base.OnStartClient();
        
        if (IsOwner)
        {
            isNetworkObject = false;
        }
        else
        {
            isNetworkObject = true;
           // LoadAvatar();
        }

        
        LoadAvatar();
    }
    public void LoadAvatar()
    {
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
        if(changeAvatar)
        {
            LoadAvatar();
            changeAvatar = false;
        }
        if(InitAgain)
        {
            InitController();
            InitAgain = false;
        }
        if(ChangeURLL)
        {
            Name = avatarUrl;
            ChangeURLL = false;
        }
    }
    void InitController()
    {
        // avatarController
    }
    private void OnLoading(object sender, ProgressChangeEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnLoadFailed(object sender, FailureEventArgs args)
    {
       
    }
    private void _OnLoading(object sender, ProgressChangeEventArgs args)
    {
        
    }
    private void OnLoadCompleted(object sender, CompletionEventArgs args)
    {   
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
    private void SetupAvatar(GameObject  targetAvatar)
    {
       
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
        if(!isNetworkObject)
        {    
            avatarController.GetComponent<vThirdPersonController>().enabled = true;
            avatarController.GetComponent<vThirdPersonInput>().enabled = true;
            avatarController.transform.GetChild(3).gameObject.SetActive(true);
        }
        avatarController.SetActive(true);
        //Debug.Log(avatar.GetComponent<AvatarData>().avatarMetadata.OutfitGender);
        Invoke("ChangeAvatarRef", 0.1f);
        
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
    /// <summary>
    /// Use this function to change url
    /// </summary>
    /// <param name="url">Pass .GLB url here</param>
    public void changeAvatarUrl(string url)
    {
        avatarUrl = url;
    }
    
}
