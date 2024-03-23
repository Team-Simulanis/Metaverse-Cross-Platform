using System.Collections;
using FF;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePictureManager : MonoBehaviour
{
    public UniGifImage uniGifImage;
    public enum ProfilePictureFormat
    {
        Sprite,
        Gif
    }

    public ProfilePictureFormat profilePictureFormat;
    public static ProfilePictureManager Instance;
    public string profileRawImageLocalStoragePath;
    public Sprite profilePicture;
    

    private void Awake()
    {
        Instance = this;
       
    }
    private void Start()
    {
        uniGifImage = GetComponent<UniGifImage>();
    }
    public IEnumerator LoadGif(string url, RawImage rawImage)
    {
        profilePictureFormat = ProfilePictureFormat.Gif;
        uniGifImage.m_rawImage = rawImage;
        profileRawImageLocalStoragePath = url;
        var aspectRationController = rawImage.gameObject.AddComponent<UniGifImageAspectController>();
        uniGifImage.m_imgAspectCtrl = aspectRationController;
        
        yield return StartCoroutine(uniGifImage.SetGifFromUrlCoroutine(url));
    }

    public void LoadSprite(RawImage sprite)
    {
        profileRawImageLocalStoragePath = Application.persistentDataPath + "/profilePic.gif";
        StartCoroutine(LoadGif(profileRawImageLocalStoragePath, sprite));
    }
    
    public Sprite GetProfilePicture()
    {
        return DataManager.Instance.userData.profileImage;
    }

    public async void SetProfilePicture(string imageUrl,Image profilePictureImage,RawImage rawImageProfilePicture)
    {
        var exe = imageUrl.Split('.');
        if (exe[^1] == "svg")
        {
            profilePictureImage.sprite = WebRequestManager.DownloadSvg(imageUrl).Result;
        }
        else
        {
            var format = await WebRequestManager.WebRequest(imageUrl);
            
            if (format.Contains("GIF"))
            {
                rawImageProfilePicture.gameObject.SetActive(true);
                profilePictureImage.gameObject.SetActive(false);

                StartCoroutine(LoadGif(imageUrl, rawImageProfilePicture));
            }
            else
            {
                rawImageProfilePicture.gameObject.SetActive(false);
                profilePictureImage.sprite = WebRequestManager.ImageDownloadRequest(imageUrl).Result;
            }
        }
    }
    

}