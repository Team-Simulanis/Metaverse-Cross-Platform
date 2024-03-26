using System;
using ReadyPlayerMe.Core;
using UnityEngine;
using static System.Net.WebRequestMethods;

namespace ReadyPlayerMe.Samples.QuickStart
{
    public class ThirdPersonLoader : MonoBehaviour
    {
        
        public delegate void UrlChanger(string url);
        public static  UrlChanger urlChanger;
        private readonly Vector3 avatarPositionOffset = new Vector3(0, -0.08f, 0);
        
        [SerializeField][Tooltip("RPM avatar URL or shortcode to load")] 
        private string avatarUrl;
        private GameObject avatar;
        private AvatarObjectLoader avatarObjectLoader;
        [SerializeField][Tooltip("Animator to use on loaded avatar")] 
        private RuntimeAnimatorController animatorController;
        [SerializeField][Tooltip("If true it will try to load avatar from avatarUrl on start")] 
        private bool loadOnStart = true;
        [SerializeField][Tooltip("Preview avatar to display until avatar loads. Will be destroyed after new avatar is loaded")]
        private GameObject previewAvatar;

        public event Action OnLoadComplete;
        public GameObject LoadingPanel;
        public static GameObject _Loadingpane;
        public static ThirdPersonLoader instance;
        public bool loadCharacter;
        public Animation animationClip;
        
        private void Start()
        {
            instance = this;
            avatarUrl = "https://cdn.simulanis.io/sso/uno/production/resources/9378a98f-e63d-4c20-8dce-180806c1def9/3DAssets/universal/2/M2.glb";
            //avatarObjectLoader.loadingpanel = LoadingPanel;
            avatarObjectLoader = new AvatarObjectLoader();
            avatarObjectLoader.OnCompleted += OnLoadCompleted;
            avatarObjectLoader.OnProgressChanged += _OnLoading;
            avatarObjectLoader.OnFailed += OnLoadFailed;
            urlChanger += changeUrl;
            
            if (previewAvatar != null)
            {
                SetupAvatar(previewAvatar);
            }
            if (loadOnStart)
            {
                LoadAvatar(avatarUrl);
            }
        }

        private void Update()
        {
            if (loadCharacter) 
            {
                _LoadAvatar();
                loadCharacter = false;
            }
            
            if(  GetComponentInChildren<Animator>())
            {
                if (animatorController != null)
                    GetComponentInChildren<Animator>().runtimeAnimatorController = animatorController;
            }
        }

        private void OnLoading(object sender, ProgressChangeEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnLoadFailed(object sender, FailureEventArgs args)
        {
            OnLoadComplete?.Invoke();
        }
        private void _OnLoading(object sender, ProgressChangeEventArgs args)
        {
            if (LoadingPanel)
            {
                LoadingPanel.SetActive(true);
            }
        }
        private void OnLoadCompleted(object sender, CompletionEventArgs args)
        {
            //LoadingPanel.SetActive(true);
            if (previewAvatar != null)
            {
                Destroy(previewAvatar);
                previewAvatar = null;
            }
            SetupAvatar(args.Avatar);
            OnLoadComplete?.Invoke();
            if (LoadingPanel)
            {
                LoadingPanel.SetActive(false);
            }

          

        }

        private void SetupAvatar(GameObject  targetAvatar)
        {
            if (avatar != null)
            {
                Destroy(avatar);
            }
            
            avatar = targetAvatar;
            // Re-parent and reset transforms
            avatar.transform.parent = transform;
            avatar.transform.localPosition = avatarPositionOffset;
            avatar.transform.localRotation = Quaternion.Euler(0, 0, 0);
            
            var controller = GetComponent<ThirdPersonController>();
            if (controller != null)
            {
                controller.Setup(avatar, animatorController);
               
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

        private void _LoadAvatar()
        {
            LoadAvatar(avatarUrl);
            
        }


    }
}
